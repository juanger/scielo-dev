
EXTRA_DIST = 

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/Utils.dll
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Debug
endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/Utils.dll
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Release
endif


LINUX_PKGCONFIG = \
	$(UTILS_PC)  

	
all: $(ASSEMBLY) $(LINUX_PKGCONFIG) 

FILES = \
	StringEncoding.cs \
	AssemblyInfo.cs \
	CodesTable.cs \
	Test/TestStringEncoding.cs \
	Test/TestCodesTable.cs \
	Test.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	./utils.pc.in 

REFERENCES =  \
	-r:System \
	$(NUNIT_LIBS)

DLL_REFERENCES = 

UTILS_PC = $(BUILD_DIR)/utils.pc
$(UTILS_PC): utils.pc
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



$(build_resx_resources) : %.resources: %.resx
	resgen '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include