
EXTRA_DIST = 

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/Markup.dll
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../PDF2Text/bin/Debug/PDF2Text.dll
BUILD_DIR = ./bin/Debug
endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/Markup.dll
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../PDF2Text/bin/Release/PDF2Text.dll
BUILD_DIR = ./bin/Release
endif


PROGRAMFILES = \
	$(PDF2TEXT_DLL) \
	$(PDF2TEXT_DLL_MDB) \
	$(UTILS_DLL) \
	$(UTILS_DLL_MDB)  
LINUX_PKGCONFIG = \
	$(MARKUP_PC)  

	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	AssemblyInfo.cs \
	MarkupHTML.cs \
	Test/TestMarkupHTML.cs \
	HTMLDocument.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	./markup.pc.in 

REFERENCES =  \
	-r:System \
	-r:System.Xml \
	$(NUNIT_LIBS)

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

MARKUP_PC = $(BUILD_DIR)/markup.pc
$(MARKUP_PC): markup.pc
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



$(build_resx_resources) : %.resources: %.resx
	resgen '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include