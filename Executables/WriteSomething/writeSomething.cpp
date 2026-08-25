#include <fstream>
#include <iostream>
#include <chrono>
#include <ctime>
#include <string>
#include <sys/stat.h>

void append(char* inputString);


int main(int argc, char* argv[]){
   
if (argc < 2)
    {
        printf("Usage: <response>\n");
        return 1;
    }
   


struct stat st;

if (stat("Logs/WriteSomethingLog.txt", &st) == 0) {
    if (st.st_size >= 100 * 1024 * 1024) {
        fprintf(stderr, "Log file is full.\n");
        return 1;
    }
}

	append(argv[1]);
	return 0;
}

void append(char* inputString){

    std::ofstream file("Logs/WriteSomethingLog.txt", std::ios::app);

    if (!file) {
        std::cerr << "Failed to open file.\n";
        return;
    }


     auto now = std::chrono::system_clock::now();
    std::time_t currentTime = std::chrono::system_clock::to_time_t(now);

    file << std::ctime(&currentTime)<< inputString << '\n'<<'\n';

    return;

}
