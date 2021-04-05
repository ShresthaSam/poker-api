# Getting Started #

See [Poker API Documentation](Documentation) for other details.

__Pre Requisite__: The computer where you will be compiling and running the Poker API source code will need to have __Visual Studio 2019 or later__ installed along with __.NET Core 3.1__. Only then the following instructions will make sense.

1. [Install these tools](#install-these-tools)
2. [Get Source Code from GitHub](#get-source-code-from-github)
3. [Launch the Application](#launch-the-application)
4. [Few Words on ```userName```, ```userId``` and ```pokerHandId```](#few-words-on-username-userid-and-pokerhandid)
5. [Troubleshoot Contact](#troubleshoot-contact)

## Install these tools ##
* __SEQ log dashboard__ (1 MINUTE INSTALL)

   This allows you to view/filter/query log events logged by Poker API.

   * Go to https://datalust.co/seq, download and install the latest version.
   * Be able to view seq dashboard at http://localhost:5341/#/events.
* __DB Browser for SQLite__ (1 MINUTE INSTALL)
   * Go to https://sqlitebrowser.org/dl/, download and install the latest version.
   * Open the browser IDE and leave it open. At this point, we don't have the database file yet.
* __```dotnet-ef``` tool__

   > __This *Entity Framework Core* tool must be installed or else the API projects will not compile correctly__
   
   Using command shell on your computer, invoke the following command to install the latest version.
   ```cmd
   dotnet tool install --global dotnet-ef
   ```
   
   If you want to check if the tool is already installed, enter ```dotnet-ef``` and you should see a logo and commands summary output. You would, however, need to update to the latest version using the following command.
   ```cmd
   dotnet tool update --global dotnet-ef
   ```
   
## Get Source Code from GitHub ##
```git clone``` or download the source code from [this repository](https://github.com/ShresthaSam/poker-api.git). You may want to fork the source code to your own repository first if you so wish.

## Launch the Application ##
* Open ```PokerApi.sln``` using Visual Studio 2019 or later
* Rebuild
* You have 3 options to start the project ```PokerApi``` - IIS Express, Kestrel or Docker. Although all should work, I would give Kestrel a try first. Start Debug Run.
* You should see __Swgager UI Page__ of the Poker API
* Click ```POST``` button on the single endpoint named __```AssignRanks```__. You can read brief description about this API endpoint and its request and response models.
* Click __```Try it out```__ button to start interacting with the API
* In the request body, enter the following request json and click __```Execute```__ button
   ```json
   [
    {
      "pokerHandId": 0,
      "card1": "2H",
      "card2": "3D",
      "card3": "5S",
      "card4": "9C",
      "card5": "KD",
      "player": {
        "userName": "Angel",
        "userId": ""
      }
    },
    {
      "pokerHandId": 0,
      "card1": "2C",
      "card2": "3H",
      "card3": "4S",
      "card4": "8C",
      "card5": "AH",
      "player": {
        "userName": "Bodhi",
        "userId": ""
      }
    }
   ]
   ```
__You should see the following response from the API__ (the ```userId``` values will vary in your case since they are newly generated ```guid``` values)

```json
[
  {
    "rank": 1,
    "player": {
      "userName": "Bodhi",
      "userId": "1c3d2d2c-75ec-4087-8377-e767179cb2cb"
    },
    "hand": "HighCard",
    "rankReason": "Card value of 14 in HighCard"
  },
  {
    "rank": 2,
    "player": {
      "userName": "Angel",
      "userId": "3a88222a-a3b1-4c9f-86d1-c87679c56a6e"
    },
    "hand": "HighCard",
    "rankReason": "Card value of 13 in HighCard"
  }
]
```

Using the __DB Browser__, click __Open Database__ button and navigate to the directory containing ```PokerApi.sln``` file. You will see a database file ```PokerDB.db``` in that directory. Select and open it.

Using the SQL editor, enter and run the following SQL query.

```sql
select
	p.player_user_name, p.player_user_id,
	c1.number || c1.suit as Card1,
	c2.number || c2.suit as Card2,
	c3.number || c3.suit as Card3,
	c4.number || c4.suit as Card4,
	c5.number || c5.suit as Card5
from card_hand h 
	inner join card c1 on h.card_id_1 = c1.card_id 
	inner join card c2 on h.card_id_2 = c2.card_id
	inner join card c3 on h.card_id_3 = c3.card_id
	inner join card c4 on h.card_id_4 = c4.card_id
	inner join card c5 on h.card_id_5 = c5.card_id
	inner join player p on h.player_user_id = p.player_user_id
```

You should see the following result from SQL query (the second column will vary in your case since it is a newly generated ```guid```)
```
player_user_name  	player_user_id                        	Card1	Card2	Card3	Card4	Card5
Angel			3a88222a-a3b1-4c9f-86d1-c87679c56a6e	2H	3D	5S	9C	KD
Bodhi			1c3d2d2c-75ec-4087-8377-e767179cb2cb	2C	3H	4S	8C	AH
```

Now, go to __SEQ dashboard__ at http://localhost:5341/#/events to see all the log events generated from various Poker API components. This will give you an idea of what is going on behind the hood. SEQ is great at showing JSON representation of C# objects and offers great query capabilities.

## Few Words on ```userName```, ```userId``` and ```pokerHandId``` ##
In every request model, you should enter ```userName``` that can be used to easily identify the corresponding response model. I would leave ```userId``` and ```pokerHandId``` to their default values of ```""``` and ```0``` respectively, at least in the first few interactions with the API.

Since the backend database requires all 3 properties to have valid values, the API generates appropriate values for ```userId``` and ```pokerHandId``` for you if you leave them as defaults.

* If you do decide to use ```userId``` after it has been first generated by the API (you can see ```userId``` value for each ```userName``` in the response), you can do so. However, you must enter ```userName``` and ```userId``` pair correctly. Otherwise, you will see database error.
* You should still leave ```pokerHandId``` as the default ```0``` because it represents an ID of a record that has not yet been created. Every request to the API is an HTTP POST, not a PUT.

## Troubleshoot Contact ##
If you are running into any issues on getting started, please do not hesitate to contact me. 


