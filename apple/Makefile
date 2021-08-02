XBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT_ROOT=./ZendeskiOS
WORKSPACE=$(PROJECT_ROOT)/ZendeskiOS.xcworkspace
TARGET=ZendeskiOS

all: lib$(TARGET).a

lib$(TARGET)-x86.a:
	$(XBUILD) -workspace $(WORKSPACE) -scheme $(TARGET) -sdk iphonesimulator -arch x86_64 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphonesimulator/lib$(TARGET).a $@

lib$(TARGET)-arm64.a:
	$(XBUILD) -workspace $(WORKSPACE) -scheme $(TARGET) -sdk iphoneos -arch arm64 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

lib$(TARGET).a: lib$(TARGET)-x86.a lib$(TARGET)-arm64.a
	xcrun -sdk iphoneos lipo -create -output $@ $^

clean: