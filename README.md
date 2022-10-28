# P2-MinionManagement
An application based around the concept of a banking app, but with an added twist of a resource management game. User plays the role as a minion commander in the Dark Lord's Army and must manage their finances to survive and prosper.

Project Overview:
You are a minion in the Dark Lord’s army. You need to perform evil deeds and manage your funds to take on the heroes. Can you survive each month?

User Stories: (MVP)
- User should be able to register a new profile, using a Username and Password (create their minion general)
- User should be able to log into their profile, using their username and password. (log into their minion and continue the game)
- User should be able to have multiple accounts (gold reserves, such as having a ‘secret stash’ as savings and ‘gold bag’ as checking)
- Users should be able to see when money gets added or removed from their account, and which account it was (getting money from Dark Lord or raids, losing money to expenses or lost raids)
- User should be able to transfer money to other accounts (move gold around)
- User should have their account deleted, if their finances are at a negative value at the end of the in-game month. (Dark Lord does not stand incompetence)
- User should receive initial funds upon registering an account.

User Stories: (Stretch Goals)
- User should be able to invest money for a chance of it multiplying/increasing (minion can hire a dragon to increase their chances in a raid)
- User should receive their monthly income (minion gets paid by Dark Lord based on their minion Ranking)
- User should have money deducted from their accounts if they do not satisfy conditions (failed raid, Dark Lord interest, ?unhappy recruits?)
- User should be able to reset password
- User should be able to perform tasks which will earn or lose them money (minion can raid camps, comparing the account information to determine winner) (gain money on success, lose money, and troops on loss)
- User should be able to apply for loans (from the Dark Lord, he will expect interest)
- User should be able to input & later edit their profile information (minion description & troop information)


Game Perspective:
As a Mid Level Minion(User) you should be able to recruit troops. You have to pay for the troop maintenance. You can send your troops to raid the enemy. If you win you get more money, and lose some troops. If you lose, you lose a lot of troops and you get no money.

Minion Ranking: The more the win, the higher your monthly income (from Dark Lord) 
6 ranks. 0, 1, 2, 3, 4, 5. 3 consecutive wins go up a rank. 2 consecutive losses you go down a rank.

Gameover condition: Your finances are red. You lose(troops mutiny, Dark lord gets sick of your shit)

User Interface:
Login/ Register. After logging in you begin the resource management phase. During the management phase, users can perform tasks, or transition to raid phase.



MVPs:
- Need to be able to register a new profile
- Need to be able to log into an existing profile
- Need to store existing user’s information
- Need to be able to edit a user’s profile.
- Need to generate a horde(savings) account and a purse(checkings) account upon user registration.
- Need to keep record of how much is deposited into each account
- Need to keep record of how much is withdrawn from each account.
- Need to be able to transfer money from one account to another
- Need to be able to transfer money to another user. (to strengthen/weaken troop morale)
- Need to be able to have a supervisor add money to your horde account. (Dark Lord)
- Need to be able to have a supervisor remove money from your account. (Dark Lord)
- Need to be able to compare a profile’s account to a different profile’s account (raids)


Stretch Goals:
- Be able to update account/profile information (password, profile picture etc)
- Be able to change color scheme from normal mode to the dark mode (FE)
- Implement Monthly System (Time Limit to regain funds, monthly allowance/upkeep)
- Be able to invest your money (hire a dragon/ Buff troops)
- Be able to use JWT Authentication
- Be able to reset passwords 
- Be able to see who sends you money (Buyable)
- Request a loan from the supervisor (Dark Lord)
- Implement Sorcery (Unknown Effects)
- Be able to perform evil deeds to get various bonuses.(Ex: scouting/pillaging)
- Be able to rank up and rank down your minion ranking.(based on raid successes)
- Morale System.(affects raid success rate)


External API: Robofriends (to generate profile pics)


Technologies Used:
C#
Html
Css
Javascipt
