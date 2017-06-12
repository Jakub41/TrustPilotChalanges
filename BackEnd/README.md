# TrustPilot Chalenges - BackEnd
TrustPilot Anagram challenge

### Introduction
My solution to the BackEnd challenge is based on C#. 

### Strategy
My strategy was to narrow down the possibilities of the magic words hinne behind the anagra. I thought that, doing a simple dictionary attack would be far too long, becasue the possible combination would be milions. 
I assumed then the anagram contains a number of characters (max 21) and so I thought that the hidden phrase should have the same amount divided in 3 words. So I make a loop what is just responsive for the print magic words, the words are not find here it's just printing out the words, the possible candidates maching the characters requirments. Well, I explain to you. Let say we have `2ebaiez43cwiifbeuevgwjv`. This HASH is ok and then I have a phrase that contains 21 characters long Max. So I just looped in it and print it on console!
I also saw the HASHES have 32 charachters, I just added 34 to the Location where it finds magic words, So when I add 34 it is ignoring the HASH and after 34 it is containing magic words. I ignored adding 34 the 2 extra spaces on the magic words. After all of that I'm printing the possible candidates in a rainbow table and then checking for a mach.    

### How it works

1. The program makes Rainbow.txt file with Three Anagram words.
2. After the step one, the program gets all the data from the file Rainbow.txt, and this data is saved into HoldAllData Variable.
3. Very important step is to find Magic words, the HoldAllData variable and the TargetHash pass to the function SearchString();
4. Last step is SearchString() in which TargetHAsh & HoldAllData are compared with each other. If any magic word it is found it is returning the position where the magic words are found, else return nothing found!
