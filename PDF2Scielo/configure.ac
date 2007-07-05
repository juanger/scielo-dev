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



MOZILLA_HOME="`$PKG_CONFIG --variable=libdir mozilla-gtkmozembed`"
if !(test -n "$MOZILLA_HOME"); then
        MOZILLA_HOME="`$PKG_CONFIG --variable=libdir firefox-gtkmozembed`"
fi
if !(test -n "$MOZILLA_HOME"); then
        MOZILLA_HOME="`$PKG_CONFIG --variable=libdir xulrunner-gtkmozembed`"
fi

if !(test -n "$MOZILLA_HOME"); then
        if test -n "$MOZILLA_FIVE_HOME"; then
                MOZILLA_HOME=$MOZILLA_FIVE_HOME
        elif grep GRE_PATH /etc/gre.d/*.conf > /dev/null ; then
                MOZILLA_HOME=$(grep -h GRE_PATH= /etc/gre.d/*.conf | cut -d '"' -f 2 -d = | head -n 1)
        elif [ $(which mozilla 2>/dev/null) ] && grep MOZILLA_FIVE_HOME= "$(which mozilla)" > /dev/null ; then
                MOZILLA_HOME=$(grep MOZILLA_FIVE_HOME= $(which mozilla) | cut -d '"' -f 2 | cut -d '=' -f 2 )
        elif [ $(which firefox 2>/dev/null) ] && grep MOZILLA_FIVE_HOME= "$(which firefox)" > /dev/null ; then
                MOZILLA_HOME=$(grep MOZILLA_FIVE_HOME= $(which firefox) | cut -d '"' -f 2 | cut -d '=' -f 2 )
        elif [ $(which firefox 2>/dev/null) ] && grep MOZILLA_LIBDIR= "$(which firefox)" > /dev/null ; then
                MOZILLA_HOME=$(grep MOZILLA_LIBDIR= $(which firefox) | cut -d '"' -f 2 | cut -d '=' -f 2)
        else
                if test "x$enable_aspnetedit" = "xyes"; then
                        AC_MSG_ERROR([Cannot detect Mozilla library directory. AspNetEdit addin cannot be built.])
                else
                        AC_MSG_WARN([Cannot detect Mozilla library directory. WelcomePage addin may not be able to function.])
                fi
        fi
fi

AC_SUBST(MOZILLA_HOME)




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
