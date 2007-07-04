AC_INIT([pdf2scielo], 0.0.0.1, [scielo-development@googlegroups.com])
AC_PREREQ(2.53)
AM_INIT_AUTOMAKE([gnu 1.9 tar-ustar])
AM_MAINTAINER_MODE

ASSEMBLY_VERSION=0.0.0.1

AC_PATH_PROG(MONO, mono)
AC_PATH_PROG(MCS, gmcs)

if test "x$MONO" = "x" ; then
        AC_MSG_ERROR([Can't find "mono" in your PATH])
fi

if test "x$MCS" = "x" ; then
  AC_MSG_ERROR([Can't find "gmcs" in your PATH])
fi

AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
        AC_MSG_ERROR([You need to install pkg-config])
fi

PKG_CHECK_MODULES([GLIB_SHARP_20], [glib-sharp-2.0])
PKG_CHECK_MODULES([GTK_SHARP_20], [gtk-sharp-2.0])
PKG_CHECK_MODULES([NUNIT], [nunit])
PKG_CHECK_MODULES([GECKO_SHARP_20], [gecko-sharp-2.0])
PKG_CHECK_MODULES([GLADE_SHARP_20], [glade-sharp-2.0])


AC_ARG_ENABLE(debug,
        AC_HELP_STRING([--enable-debug],
                [Use 'DEBUG' Configuration [default=YES]]),
                enable_debug=yes, enable_debug=no)
AM_CONDITIONAL(ENABLE_DEBUG, test x$enable_debug = xyes)
if test "x$enable_debug" = "xyes" ; then
        CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(release,
        AC_HELP_STRING([--enable-release],
                [Use 'RELEASE' Configuration [default=NO]]),
                enable_release=yes, enable_release=no)
AM_CONDITIONAL(ENABLE_RELEASE, test x$enable_release = xyes)
if test "x$enable_release" = "xyes" ; then
        CONFIG_REQUESTED="yes"
fi
if test -z "$CONFIG_REQUESTED" ; then
        AM_CONDITIONAL(ENABLE_DEBUG, true)
fi


AC_CONFIG_FILES([
Utils/Makefile
Markup/markup.pc
PDFImage/pdfimage.pc
PDF2Text/pdf2text.pc
PDF2Scielo/pdf2scielo
PDFImage/Makefile
Utils/utils.pc
Makefile
PDF2Text/Makefile
PDF2Scielo.Gui/pdf2scielo-gui
Markup/Makefile
PDF2Scielo.Gui/Makefile
PDF2Scielo/Makefile

])

AC_OUTPUT
