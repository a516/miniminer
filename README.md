MiniMiner
=====================

This project is just a test project for a c# developer to get a handle on writing a bitcoin miner. At any point, the code may be highly unstable, so use at your own risk.

The base code was copied from Lithander. I did some refactoring to make things a bit more readable and object oriented. Things I added.

a get work queue.
This queue adds work from the pool and has them ready for when a worker needs it. It initially queues up the work on start. Then when a worker grabs work off the queue an asynchronous thread fires to get the new job in the background.
a send work Queue

This queue sends work to the pool and then displays the status to the console. Instead of having a work thread wait for the confirmation, the worker will add this to the queue. A background thread then fires to send the work and get and display confirmation
So far I've added a threading implementation. I realized that my threading implementation may not be the most optimal. The current implementation creates many workers to work on different batches of work. Although this does utilize more of the cpu, it doesn't solve one batch faster, although it does solve several batches concurrently.