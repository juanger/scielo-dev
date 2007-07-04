
EXTRA_DIST = 

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/pdf2scielo.exe
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../PDF2Text/bin/Debug/PDF2Text.dll \
	../Markup/bin/Debug/Markup.dll
BUILD_DIR = ./bin/Debug
endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/pdf2scielo.exe
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../PDF2Text/bin/Release/PDF2Text.dll \
	../Markup/bin/Release/Markup.dll
BUILD_DIR = ./bin/Release
endif


BINARIES = \
	$(PDF2SCIELO)  
PROGRAMFILES = \
	$(PDF2TEXT_DLL) \
	$(PDF2TEXT_DLL_MDB) \
	$(UTILS_DLL) \
	$(UTILS_DLL_MDB) \
	$(MARKUP_DLL) \
	$(MARKUP_DLL_MDB)  

	
all: $(ASSEMBLY) $(BINARIES) $(PROGRAMFILES) 

FILES = \
	Driver.cs \
	AssemblyInfo.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	./pdf2scielo.in 

REFERENCES =  \
	-r:System

DLL_REFERENCES = 

PDF2TEXT_DLL = $(BUILD_DIR)/PDF2Text.dll
$(PDF2TEXT_DLL): ../PDF2Text/bin/Debug/PDF2Text.dll
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

PDF2TEXT_DLL_MDB = $(BUILD_DIR)/PDF2Text.dll.mdb
$(PDF2TEXT_DLL_MDB): ../PDF2Text/bin/Debug/PDF2Text.dll.mdb
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

UTILS_DLL = $(BUILD_DIR)/Utils.dll
$(UTILS_DLL): ../Utils/bin/Debug/Utils.dll
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

UTILS_DLL_MDB = $(BUILD_DIR)/Utils.dll.mdb
$(UTILS_DLL_MDB): ../Utils/bin/Debug/Utils.dll.mdb
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

MARKUP_DLL = $(BUILD_DIR)/Markup.dll
$(MARKUP_DLL): ../Markup/bin/Debug/Markup.dll
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

MARKUP_DLL_MDB = $(BUILD_DIR)/Markup.dll.mdb
$(MARKUP_DLL_MDB): ../Markup/bin/Debug/Markup.dll.mdb
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

PDF2SCIELO = $(BUILD_DIR)/pdf2scielo
$(PDF2SCIELO): pdf2scielo
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



$(build_resx_resources) : %.resources: %.resx
	resgen '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include