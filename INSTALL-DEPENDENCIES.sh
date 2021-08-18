REPO="https://zendesk.jfrog.io/ui/native/repo/com/zendesk"

CHAT_SOCKET_CLIENT_VERSION="6.1.0"
CHAT_VERSION="3.2.0"
CHAT_PROVIDERS_VERSION="3.3.0"
CHAT_VISTOR_CLIENT_VERSION="8.3.0"
COMMON_UI_VERSION="4.0.2"
JAVA_COMMON_VERSION="2.0.0"
MESSAGING_VERSION="5.2.0"
MESSAGING_API_VERSION="5.2.0"
SDK_CONFIGURATIONS_VERSION="2.0.1"

echo ""
echo "Installing Android Dependencies…"

echo ""
echo "Downloading chat socket client version ${CHAT_SOCKET_CLIENT_VERSION}"
curl "${REPO}/chat-socket-client/${CHAT_SOCKET_CLIENT_VERSION}/chat-socket-client-${CHAT_SOCKET_CLIENT_VERSION}.jar" -s -o android/chat-socket-client.jar

echo ""
echo "Downloading chat version ${CHAT_VERSION}"
curl "${REPO}/chat/${CHAT__VERSION}/chat-${CHAT_VERSION}.aar" -s -o android/chat.aar

echo ""
echo "Downloading chat providers version ${CHAT_PROVIDERS_VERSION}"
curl "${REPO}/chat-providers/${CHAT_PROVIDERS_VERSION}/chat-providers-${CHAT_PROVIDERS_VERSION}.aar" -s -o android/chat-providers.aar

echo ""
echo "Downloading chat visitor client version ${CHAT_VISTOR_CLIENT_VERSION}"
curl "${REPO}/chat-visitor-client/${CHAT_VISTOR_CLIENT_VERSION}/chat-visitor-client-${CHAT_VISTOR_CLIENT_VERSION}.jar" -s -o android/chat-visitor-client.jar

echo ""
echo "Downloading common UI version ${COMMON_UI_VERSION}"
curl "${REPO}/common-ui/${COMMON_UI_VERSION}/common-ui-${COMMON_UI_VERSION}.aar" -s -o android/common-ui.aar

echo ""
echo "Downloading Java common version ${JAVA_COMMON_VERSION}"
curl "${REPO}/java-common/${JAVA_COMMON_VERSION}/java-common-${JAVA_COMMON_VERSION}.jar" -s -o android/java-common.jar

echo ""
echo "Downloading messaging version ${MESSAGING_VERSION}"
curl "${REPO}/messaging/${MESSAGING_VERSION}/messaging-${MESSAGING_VERSION}.aar" -s -o android/messaging.aar

echo ""
echo "Downloading messaging API version ${MESSAGING_API_VERSION}"
curl "${REPO}/messaging-api/${MESSAGING_API_VERSION}/messaging-api-${MESSAGING_API_VERSION}.aar" -s -o android/messaging-api.aar

echo ""
echo "Downloading SDK configurations version ${SDK_CONFIGURATIONS_VERSION}"
curl "${REPO}/sdk-configurations/${SDK_CONFIGURATIONS_VERSION}/sdk-configurations-${SDK_CONFIGURATIONS_VERSION}.aar" -s -o android/sdk-configurations.aar

