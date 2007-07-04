
EXTRA_DIST = 

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/PDF2Text.dll
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../Utils/bin/Debug/Utils.dll
BUILD_DIR = ./bin/Debug
endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/PDF2Text.dll
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../Utils/bin/Release/Utils.dll
BUILD_DIR = ./bin/Release
endif


PROGRAMFILES = \
	$(UTILS_DLL) \
	$(UTILS_DLL_MDB)  
LINUX_PKGCONFIG = \
	$(PDF2TEXT_PC)  

	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	AssemblyInfo.cs \
	IExtractable.cs \
	INormalizable.cs \
	AtmNormalizer.cs \
	Test/TestAtmNormalizer.cs \
	PDFPoppler.cs \
	Test/TestPDFPoppler.cs \
	Document.cs \
	RawDocument.cs \
	NormDocument.cs \
	Test/TestRawDocument.cs \
	Test/TestNormDocument.cs \
	StyleReader.cs \
	PDFTextColumn.cs \
	Test/TestPDFTextColumn.cs 

DATA_FILES = 

RESOURCES = \
	Data/style.dtd \
	Data/style.xsd 

EXTRAS = \
	Test/test.xml \
	Test/test-schema.xml \
	Test/unit-test.sources \
	Test/atm/v17n01a02.htm \
	Test/atm/v17n01a02.pdf \
	Test/atm/v17n01a02-norm.txt \
	Test/atm/v17n01a02-raw.txt \
	Test/atm/v17n2a02.htm \
	Test/atm/v17n2a02.pdf \
	Test/atm/v17n2a02-norm.txt \
	Test/atm/v17n2a02-raw.txt \
	Test/atm/v17n4a03.htm \
	Test/atm/v17n4a03.pdf \
	Test/atm/v17n4a03-norm.txt \
	Test/atm/v17n4a03-raw.txt \
	Test/atm/atm.sources \
	./pdf2text.pc.in 

REFERENCES =  \
	-r:System \
	$(NUNIT_LIBS) \
	-r:System.Xml

DLL_REFERENCES = 

UTILS_DLL = $(BUILD_DIR)/Utils.dll
$(UTILS_DLL): ../Utils/bin/Debug/Utils.dll
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

UTILS_DLL_MDB = $(BUILD_DIR)/Utils.dll.mdb
$(UTILS_DLL_MDB): ../Utils/bin/Debug/Utils.dll.mdb
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

PDF2TEXT_PC = $(BUILD_DIR)/pdf2text.pc
$(PDF2TEXT_PC): pdf2text.pc
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



$(build_resx_resources) : %.resources: %.resx
	resgen '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY).mdb: $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$@ -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

include $(top_srcdir)/Makefile.include