using System.Net;

namespace MiniMiner
{
    internal class MidStateModule
    {
        private class Sha256StateT
        {
            internal uint[] h;
            private byte[] _b = new byte[32];

            public Sha256StateT()
            {
                h = new uint[8];
            }
        }


        private static uint[] h = new uint[]
            {0x6a09e667, 0xbb67ae85, 0x3c6ef372, 0xa54ff53a, 0x510e527f, 0x9b05688c, 0x1f83d9ab, 0x5be0cd19};

        private static uint[] k = new uint[]
            {
                0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
                0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
                0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
                0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
                0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
                0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
                0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
                0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
            };

        private static uint ror32(uint v, uint n)
        {
            var v1 = v;
            var v2 = v;
            var n1 = (int) n;
            var n2 = (int) n;
            return (v1 >> n1) | (v2 << (32 - n2));
        }

        private static void update_state(Sha256StateT state, uint[] data)
        {
            var w = new uint[64];
            var t = state;

            for (var i = 0; i < 16; i++)
            {
                w[i] = (uint) IPAddress.NetworkToHostOrder(data[i]);
            }

            for (var i = 16; i < 64; i++)
            {
                var s0 = ror32(w[i - 15], 7) ^ ror32(w[i - 15], 18) ^ (w[i - 15] >> 3);
                var s1 = ror32(w[i - 2], 17) ^ ror32(w[i - 2], 19) ^ (w[i - 2] >> 10);
                w[i] = w[i - 16] + s0 + w[i - 7] + s1;
            }

            for (var i = 0; i < 64; i++)
            {
                var s0 = ror32(t.h[0], 2) ^ ror32(t.h[0], 13) ^ ror32(t.h[0], 22);
                var maj = (t.h[0] & t.h[1]) ^ (t.h[0] & t.h[2]) ^ (t.h[1] & t.h[2]);
                var t2 = s0 + maj;
                var s1 = ror32(t.h[4], 6) ^ ror32(t.h[4], 11) ^ ror32(t.h[4], 25);
                var ch = (t.h[4] & t.h[5]) ^ (~t.h[4] & t.h[6]);
                var t1 = t.h[7] + s1 + ch + k[i] + w[i];

                t.h[7] = t.h[6];
                t.h[6] = t.h[5];
                t.h[5] = t.h[4];
                t.h[4] = t.h[3] + t1;
                t.h[3] = t.h[2];
                t.h[2] = t.h[1];
                t.h[1] = t.h[0];
                t.h[0] = t1 + t2;
            }

            for (var i = 0; i < 8; i++)
            {
                state.h[i] += t.h[i];
            }
        }

        private static void init_state(out Sha256StateT state)
        {
            state = new Sha256StateT();
            for (var i = 0; i < 8; i++)
            {
                state.h[i] = h[i];
            }
        }

        private static Sha256StateT midstate(uint[] data)
        {
            Sha256StateT state;
            init_state(out state);
            update_state(state, data);
            return state;
        }

        //void print_hex(uint[] data, size_t s) {
        //    for (var i = 0; i < s; i++) {
        //        Console.Write("{0}", data[i]);
        //    }
        //    Console.WriteLine();
        //}

        //PyObject *midstate_helper(PyObject *self, PyObject *arg) {
        //    Py_ssize_t s;
        //    PyObject *ret = NULL;
        //    PyObject *t_int = NULL;
        //    char *t;
        //    unsigned char data[64];
        //    sha256_state_t mstate;

        //    if (PyBytes_Check(arg) != true) { 
        //        PyErr_SetString(PyExc_ValueError, "Need bytes object as argument.");
        //        goto error; 
        //    }
        //    if (PyBytes_AsStringAndSize(arg, &t, &s) == -1) {
        //        // Got exception
        //        goto error;
        //    }
        //    if (s < 64) { 
        //        PyErr_SetString(PyExc_ValueError, "Argument length must be at least 64 bytes.");
        //        goto error; 
        //    }

        //    memcpy(data, t, 64);
        //    mstate = midstate(data);

        //    ret = PyTuple_New(8);
        //    for (size_t i = 0; i < 8; i++) {
        //        t_int = PyLong_FromUnsignedLong(mstate.h[i]);
        //        if (PyTuple_SetItem(ret, i, t_int) != 0) { 
        //            t_int = NULL; // ret is owner of the int now
        //            goto error; 
        //        }
        //    }

        //    return ret;

        //error:
        //    Py_XDECREF(t_int);
        //    Py_XDECREF(ret);

        //    return NULL;
        //}

        //static struct PyMethodDef midstate_functions[] = {
        //    {"SHA256", midstate_helper, METH_O, NULL},
        //    {NULL, NULL, 0, NULL},
        //};


        //    static struct PyModuleDef moduledef = {
        //        PyModuleDef_HEAD_INIT,
        //        "midstate",
        //        NULL,
        //        -1,
        //        midstate_functions,
        //        NULL,
        //        NULL,
        //        NULL,
        //        NULL,
        //    };

        //    PyInit_midstate(void)
        //    {
        //        return PyModule_Create(&midstatemodule);
        //    }
    }
}
