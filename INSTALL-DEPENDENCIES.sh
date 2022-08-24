API="https://zendesk.jfrog.io/ui/api/v1/download?repoKey=repo"

CHAT_SOCKET_CLIENT_VERSION="6.1.0"
CHAT_VERSION="3.2.0"
CHAT_PROVIDERS_VERSION="3.3.0"
CHAT_VISTOR_CLIENT_VERSION="8.3.0"
COMMON_UI_VERSION="4.0.2"
JAVA_COMMON_VERSION="2.0.0"
MESSAGING_VERSION="5.2.0"
MESSAGING_API_VERSION="5.2.0"
SDK_CONFIGURATIONS_VERSION="2.0.1"

mkdir android

echo ""
echo ""
echo "Installing Android Dependencies…"

echo ""
echo "Downloading chat socket client version ${CHAT_SOCKET_CLIENT_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fchat-socket-client%2F${CHAT_SOCKET_CLIENT_VERSION}%2Fchat-socket-client-${CHAT_SOCKET_CLIENT_VERSION}.jar" -O android/chat-socket-client.jar

echo ""
echo "Downloading chat version ${CHAT_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fchat%2F${CHAT_VERSION}%2Fchat-${CHAT_VERSION}.aar" -O android/chat.aar

echo ""
echo "Downloading chat providers version ${CHAT_PROVIDERS_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fchat-providers%2F${CHAT_PROVIDERS_VERSION}%2Fchat-providers-${CHAT_PROVIDERS_VERSION}.aar" -O android/chat-providers.aar

echo ""
echo "Downloading chat visitor client version ${CHAT_VISTOR_CLIENT_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fchat-visitor-client%2F${CHAT_VISTOR_CLIENT_VERSION}%2Fchat-visitor-client-${CHAT_VISTOR_CLIENT_VERSION}.jar" -O android/chat-visitor-client.jar

echo ""
echo "Downloading common UI version ${COMMON_UI_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fcommon-ui%2F${COMMON_UI_VERSION}%2Fcommon-ui-${COMMON_UI_VERSION}.aar" -O android/common-ui.aar

echo ""
echo "Downloading Java common version ${JAVA_COMMON_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fjava-common%2F${JAVA_COMMON_VERSION}%2Fjava-common-${JAVA_COMMON_VERSION}.jar" -O android/java-common.jar

echo ""
echo "Downloading messaging version ${MESSAGING_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fmessaging%2F${MESSAGING_VERSION}%2Fmessaging-${MESSAGING_VERSION}.aar" -O android/messaging.aar

echo ""
echo "Downloading messaging API version ${MESSAGING_API_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fmessaging-api%2F${MESSAGING_API_VERSION}%2Fmessaging-api-${MESSAGING_API_VERSION}.aar" -O android/messaging-api.aar

echo ""
echo "Downloading SDK configurations version ${SDK_CONFIGURATIONS_VERSION}"
wget "${API}&path=com%2Fzendesk%2Fsdk-configurations%2F${SDK_CONFIGURATIONS_VERSION}%2Fsdk-configurations-${SDK_CONFIGURATIONS_VERSION}.aar" -O android/sdk-configurations.aar

echo ""
echo ""
echo "Installing iOS Dependencies…"

cd apple
git submodule update --init --recursive
cd ..

echo ""
echo ""
echo "Done!"
