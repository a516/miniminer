
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug "-define:DEBUG;TRACE"
ASSEMBLY = bin/Debug/MiniMiner.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug/

MINIMINER_EXE_MDB_SOURCE=bin/Debug/MiniMiner.exe.mdb
MINIMINER_EXE_MDB=$(BUILD_DIR)/MiniMiner.exe.mdb
MINIMINER_EXE_CONFIG_SOURCE=app.config

endif

ifeq ($(CONFIG),DEBUG_X86)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;TRACE"
ASSEMBLY = bin/Debug/MiniMiner.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug/

MINIMINER_EXE_MDB_SOURCE=bin/Debug/MiniMiner.exe.mdb
MINIMINER_EXE_MDB=$(BUILD_DIR)/MiniMiner.exe.mdb
MINIMINER_EXE_CONFIG_SOURCE=app.config

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ "-define:TRACE"
ASSEMBLY = bin/Release/MiniMiner.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/

MINIMINER_EXE_MDB=
MINIMINER_EXE_CONFIG_SOURCE=app.config

endif

ifeq ($(CONFIG),RELEASE_X86)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ "-define:TRACE"
ASSEMBLY = bin/Release/MiniMiner.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/

MINIMINER_EXE_MDB=
MINIMINER_EXE_CONFIG_SOURCE=app.config

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MINIMINER_EXE_MDB) \
	$(MINIMINER_EXE_CONFIG)  

BINARIES = \
	$(MINIMINER)  


RESGEN=resgen2

MINIMINER_EXE_CONFIG = $(BUILD_DIR)/MiniMiner.exe.config
MINIMINER = $(BUILD_DIR)/miniminer

FILES =  \
	ConcurrentQueue.cs \
	MidStateModule.cs \
	Pool.cs \
	Program.cs \
	Properties/AssemblyInfo.cs \
	SendWorkQueue.cs \
	Share.cs \
	Utils.cs \
	Work.cs \
	Worker.cs \
	WorkQueue.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	app.config \
	miniminer.in 

REFERENCES =  \
	System \
	System.Configuration \
	System.Core \
	System.Data \
	System.Data.DataSetExtensions \
	System.Xml \
	System.Xml.Linq

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,MINIMINER_EXE_CONFIG))
$(eval $(call emit-deploy-wrapper,MINIMINER,miniminer,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MINIMINER_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MINIMINER_EXE_CONFIG),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(bindir)'
	$(call cp,$(MINIMINER),$(DESTDIR)$(bindir))
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MINIMINER_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MINIMINER_EXE_CONFIG),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MINIMINER),$(DESTDIR)$(bindir))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
