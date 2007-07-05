
EXTRA_DIST =

#Warning: This is an automatically generated file, do not edit!
if ENABLE_DEBUG
 SUBDIRS =  PDFImage Utils PDF2Text Markup PDF2Scielo PDF2Scielo.Gui
endif
if ENABLE_RELEASE
 SUBDIRS =  PDFImage Utils PDF2Text Markup PDF2Scielo PDF2Scielo.Gui
endif

run:
	cd PDF2Scielo.Gui/bin/Debug &&  LD_LIBRARY_PATH=`echo "$(LD_LIBRARY_PATH):$(MOZILLA_HOME)" | sed 's/^://g'` mono --debug pdf2scielo-gui.exe
