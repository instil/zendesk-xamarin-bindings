#!/usr/bin/env bash

set -e

cd $(dirname $0)

echo "Building iOS framework"
xcodebuild -workspace ZendeskiOS/ZendeskiOS.xcworkspace -scheme ZendeskiOS -configuration Release -sdk iphoneos clean build \
  CONFIGURATION_BUILD_DIR=$(pwd)/build/frameworks/iOS

xcodebuild -workspace ZendeskiOS/ZendeskiOS.xcworkspace -scheme ZendeskiOS -configuration Release -sdk iphonesimulator clean build \
  CONFIGURATION_BUILD_DIR=$(pwd)/build/frameworks/iOS/simulator

cd build/frameworks/iOS

if [ -d "simulator/ZendeskiOS.framework/Modules/ZendeskiOS.swiftmodule" ]; then
  cp -r simulator/ZendeskiOS.framework/Modules/ZendeskiOS.swiftmodule/* ZendeskiOS.framework/Modules/ZendeskiOS.swiftmodule/
fi

lipo -create -output libZendeskiOS.a libZendeskiOS.a simulator/libZendeskiOS.a

rm -rf simulator