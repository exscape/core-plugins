include $(top_srcdir)/build.rules.mk

ASSEMBLY=Rhythmbox

FILES = \
	src/MusicItems.cs \
	src/Rhythmbox.cs \
	src/RhythmboxDBus.cs \
	src/EnqueueAction.cs \
	src/RhythmboxItems.cs \
	src/PlayAction.cs \
	src/MusicItemSource.cs \
	src/AbstractPlaybackAction.cs \
	src/NextAction.cs \
	src/PauseAction.cs \
	src/PreviousAction.cs \
	src/PlayItemAction.cs 

GENERATED_RESOURCES = \
	Resources/Rhythmbox.addin.xml 

REFERENCES = \
	System \
	System.Core \
	System.Xml \
	$(DBUS_SHARP_LIBS) \
	$(GTK_SHARP_20_LIBS) \
	$(DO_PLATFORM_LIBS) \
	$(DO_UNIVERSE_LIBS)
