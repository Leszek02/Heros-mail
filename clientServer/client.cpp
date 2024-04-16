#include "pch.h"
#include <iostream>
#include <winsock2.h>
#include <string>
#include <combaseapi.h>

#pragma comment(lib, "ws2_32.lib")

struct Email
{
	char from[256];
	char to[256];
	char title[256];
	char message[1024];
};

SOCKET fdd;
WSADATA wdd;
struct sockaddr_in saa;


extern "C" __declspec(dllexport) int Connect(SOCKET & fd, sockaddr_in & sa, const char* ipAddress, const char* username) {
	int port = 12345;
	WSAStartup(MAKEWORD(2, 2), &wdd);
	hostent* he;
	he = gethostbyname(ipAddress);
	fd = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	sa.sin_family = AF_INET;
	memcpy(&sa.sin_addr.s_addr, he->h_addr, he->h_length);
	sa.sin_port = htons(port);

	if (connect(fd, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR) {
		closesocket(fd);
		fd = INVALID_SOCKET;
		return 0;
	}

	int bytes = strlen(username);
	send(fd, username, bytes, 0);
	return 1;
}

extern "C" __declspec(dllexport) void sendMessage(const char* title, const char* from, const char* content, const char* to, SOCKET fd) {
	send(fd, "1", 1, 0);
	Email newEmail;
	strcpy(newEmail.from, from);
	strcpy(newEmail.to, to);
	strcpy(newEmail.title, title);
	strcpy(newEmail.message, content);
	send(fd, reinterpret_cast<const char*>(&newEmail), sizeof(newEmail), 0);
}

extern "C" __declspec(dllexport) char* getSendTitles(SOCKET fd) {
	char buf[1024];
	send(fd, "2", 1, 0);
	int response = recv(fd, buf, sizeof(buf), 0);
	buf[response] = '\0';
	char* result = static_cast<char*>(CoTaskMemAlloc(response + 1));
	strcpy(result, buf);
	return result;
}

extern "C" __declspec(dllexport) char* getReceivedTitles(SOCKET fd) {
	char buf[1024];
	send(fd, "3", 1, 0);
	int response = recv(fd, buf, sizeof(buf), 0);
	buf[response] = '\0';
	char* result = static_cast<char*>(CoTaskMemAlloc(response + 1));
	strcpy(result, buf);
	return result;
}

extern "C" __declspec(dllexport) void getSenderEmailContent(SOCKET fd, Email & content, const char* title) {
	send(fd, "4", 1, 0);
	int bytes = strlen(title);
	send(fd, title, bytes, 0);
	int response = recv(fd, reinterpret_cast<char*>(&content), sizeof(Email), 0);
}

extern "C" __declspec(dllexport) void getReceiverEmailContent(SOCKET fd, Email & content, const char* title) {
	send(fd, "5", 1, 0);
	int bytes = strlen(title);
	send(fd, title, bytes, 0);
	int response = recv(fd, reinterpret_cast<char*>(&content), sizeof(Email), 0);
}

extern "C" __declspec(dllexport) void disconnect(SOCKET fd) {
	send(fd, "6", 1, 0);
	closesocket(fd);
}