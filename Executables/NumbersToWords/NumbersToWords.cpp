#include <cstring>
#include <iostream>
#include <cstdio>
#include <cstdlib>

void W2N();
void N2W(char *inputString);
void putTheSegment(int num, char *result);
int doThis(int result, char *token);

// Let's code what number will receive what word (create a lookup table)
// The four declarations below are suffecient to create define any number in words from one upto hundred trillion
// ten and hundred are handled individually but kept in the following declaration in case code needs to change

const char onesWords[10][15] = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"};
const char ten2twentywords[10][15] = {"eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"};
const char ties[8][15] = {"twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};
const char segments[5][15] = {" ", "thousand", "million", "billion", "trillion"};

//  int main()
//  {
//  char input[3];
//  printf("W2N or N2W \n");
//  scanf("%s",input);
//  if(std::strcmp(input, "W2N") == 0) W2N();
//  else if(std::strcmp(input, "N2W") == 0) N2W();
//  else printf("Input not recognized");
//  N2W();
//  exit(0);
//  }

// Code was modified to accept command line input
int main(int argc, char *argv[])
{
    if (argc < 2)
    {
        // unnecessary but i like following standards
        printf("Usage:  <number>\n");
        return 1;
    }

    N2W(argv[1]);

    return 0;
}

// Number to words
void N2W(char *inputString)
{
    long long input = atoll(inputString);
    int i = 0;
    //  printf("Enter the numbers \n");
    //  scanf("%lld",&input);

    // handle zero separately
    if (input == 0)
    {
        printf("zero \n");
        return;
        
    }
    else if (input < 0)  // negative input will return the output 'Minus' + regular number conversion
    {
        printf("Minus ");
        input = -input;
    }

    // threes is a variable to store segment of 3 digits from right to left ex: 1234567 converted to 1,234,567
    int threes[5] = {0};
    while (input != 0)
    {

        // check if the segment of 3 digits is more than 5 (more than trillions)
        if (i > 4)
        {
            printf("Input exceeds trillions \n");
            return;
        }
        // divide input into segment of 3 digit numbers
        threes[i] = input % 1000;
        input = input / 1000;
        i++;
    }
    i = 4;
    for (int i = 4; i >= 0; i--)
    {
        // If any segment is 0, just move forward ex: 1,000,567. Second segment is skipped
        if (threes[i] == 0)
            continue;

        // result shouldn't exceed 50 characters
        char result[50];

        // pass the 3 digit number segments and result variable
        putTheSegment(threes[i], result);

        // print the result, this is the ouput of the program
        printf("%s %s ", result, segments[i]);
    }
    // N2W();
}

//  void W2N() {

//     char input[255];
// printf("Enter the words \n");
//     fflush(stdout);
//     input[strcspn(input, "\n")] = '\0';

//     fgets(input, sizeof(input), stdin);
//     input[strcspn(input, "\n")] = '\0';
//     int result=0;
//     printf("%s\n",input);
//     char *token=strtok(input," ");
//     while(token!=NULL) {
// // result=doTHis(result,token);
// token=strtok(NULL," ");
//     }

// }

// check for appropriate word for the number
int doThis(int result, char *token)
{
    // check one to ten
    for (int i = 0; i < 10; i++)
    {
        if (strcmp(token, onesWords[i]) == 0)
        {
            result += i + 1;
            return result;
        }
    }

    // check eleven to nineteen
    for (int i = 0; i < 9; i++)
    {
        if (strcmp(token, ten2twentywords[i]) == 0)
        {
            result += i + 11;
            return result;
        }
    }

    // check twenty, thirty, forty, etc.
    for (int i = 0; i < 8; i++)
    {
        if (strcmp(token, ties[i]) == 0)
        {
            result += (i + 2) * 10;
            return result;
        }
    }

    return result;
}

void putTheSegment(int num, char *result)
{

    // break the segment of 3 digits into single digits now ex: 567 converted to 5,6,7
    int res1 = num % 10;
    num /= 10;
    int res2 = num % 10;
    num /= 10;
    int res3 = num % 10;
    num /= 10;

    result[0] = '\0';

    if (res3 != 0) //ex 1xx
    {
        // take the first digit of the segment, and if it is not 0, find the appropriate word and tack on 'hundred' ex: 230 converted to two hundred (followed by the code below)
        strcpy(result, onesWords[res3 - 1]);
        strcat(result, " hundred ");
    }
    // hundreds place is taken of, now we just need to mamage tens and ones place


    // check if ones place is not zero ex: not xx2
    if (res1 != 0)
    {
        if (res2 == 0) // if ones place is not zero but tens pace is not zero ex: x0x
        {
            strcat(result, onesWords[res1 - 1]);
        }
        
        else if (res2 == 1) // if tens place is 1 ex: x1x, it needs to be handled separately because 10-19 words don't seem to follow a standard
        {
            strcat(result, ten2twentywords[res1 - 1]);
        }
        
        else // if tens place is not zero or 1. ex: xxx
        {
            strcat(result, ties[res2 - 2]);
            strcat(result, " ");
            strcat(result, onesWords[res1 - 1]);
        }
    }
    // if ones place is 0. At this point, hundreds, and ones place is already handled so we just need to focucs on tens place

    else // case when ones place is 0 ex: xx0
    { 
        
        if (res2 == 1) // check if tens place is a 1. ex: x10
        {
            strcat(result, "ten");
        }
        else if (res2 > 1) // check if tens place is not 1 ex: xx0
        {
            strcat(result, ties[res2 - 2]);
        }
    }
}