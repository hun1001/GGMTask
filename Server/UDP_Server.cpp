#define _WINSOCKAPI_
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include "msgpack.hpp"
#include <windows.h>
#include <winsock2.h>
#include <iostream>
#include <thread>

#define int32 __int32

#pragma comment(lib, "ws2_32.lib")

using namespace std;

void HandleError(const char* cause) {
	int32 errCode = ::WSAGetLastError();
	cout << cause << " Socket ErrorCode: " << errCode << endl;
}

int main()
{
	// 원속 초기화 (ws2_32 라이브러리 초기화)
	// 관련 정보가 wsaData에 채워짐
	WSAData wsaData;
	if (::WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		return 0;

	SOCKET serverSocket = ::socket(AF_INET, SOCK_DGRAM, 0);
	if (serverSocket == INVALID_SOCKET) {
		HandleError("Socket");
		return 0;
	}

	// 연결할 목적지 -> IP + Port ex) XX아파트 YY 호
	SOCKADDR_IN serverAddr; // IPv4
	::memset(&serverAddr, 0, sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_addr.s_addr = ::htonl(INADDR_ANY);
	serverAddr.sin_port = ::htons(7777);

	if (::bind(serverSocket, (SOCKADDR*)&serverAddr, sizeof(serverAddr)) == SOCKET_ERROR) {
		HandleError("Bind");
	}

	while (true) {
		SOCKADDR_IN clientAddr;
		::memset(&clientAddr, 0, sizeof(clientAddr));
		int32 addrLen = sizeof(clientAddr);

		this_thread::sleep_for(1s);

		char recvBuffer[1000];
		int32 recvLen = ::recvfrom(serverSocket, recvBuffer, sizeof(recvBuffer), 0,
			(SOCKADDR*)&clientAddr, &addrLen);

		if (recvLen <= 0) {
			HandleError("RecvFrom");
			return 0;
		}

		cout << "Recv Data: " << recvBuffer << endl;
		cout << "Recv Len: " << recvLen << endl;
		cout << "Client IP: " << ::inet_ntoa(clientAddr.sin_addr) << ":" << clientAddr.sin_port << endl;

		int32 errorCode = ::sendto(serverSocket, recvBuffer, recvLen, 0,
			(SOCKADDR*)&clientAddr, sizeof(clientAddr));

		if (errorCode == SOCKET_ERROR) {
			HandleError("SendTo");
			return 0;
		}

		cout << "Send Data! Len: " << recvLen << endl;
	}

	// 윈속 종료
	::WSACleanup();
}
