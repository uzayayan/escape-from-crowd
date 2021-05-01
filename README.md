# Introduction #

Hi Firebyte Teams!

First of all, the game must be launched from the "Initializer" scene for it to work correctly. You can find Game Settings, Levels and Weapons presets in Resources folder. By the way I created 2 level and 2 weapon data for the example. Thanks for taking your time to examine.

Enjoy!

# Overview #

Unity Version : 2019.4.23f1
- Dependent Packages : 
  - DOTween
  - UniTask
  - MyBox


# Game Settings #

![alt tag](https://i.ibb.co/r3JQdgy/Screen-Shot-2021-05-01-at-22-56-11.png)
  
    - Player Speed : This field helper for change speed of player.
    - Enemy Speed : This field helper for change speed of enemys.
    - Wall Prefab : This field reference of wall prefab.
    - Gem Prefab : This field reference of gem prefab.
    - Enemy Prefab : This field reference of enemy prefab.
    - Boss Prefab : This field reference of boss prefab.

Notice : 
  Some statics fields included in CommonTypes script :
  
    - PLAYER_SPEED_MULTIPLIER
    - ENEMY_SPEED_MULTIPLIER
    - CAMERA_SPEED
    - FLY_GEM_TIME
    - AREA_SIZE_MULTIPLIER
    - etc.


# Create Level #

You can create level in easly. Steps:

### 1 - Create New Level Object ###
![alt tag](https://i.ibb.co/DKhBrKy/Screen-Shot-2021-05-01-at-23-12-36.png)

### 2 - Fill Fields of Level ###
![alt tag](https://i.ibb.co/CsWjrFp/Screen-Shot-2021-05-01-at-23-16-52.png)
  
    - Id : This field is should be uniqe and in order.
    - Play Area : This field is indicates objects that will appear in front of the player.
    - Enemy Area : This field is indicates objects that will appear in behind of the player.
    - Skybox : This field is determines weather of the level.

### 3 - Last Step ###
![alt tag](https://i.ibb.co/mH5D2Fn/Screen-Shot-2021-05-01-at-23-40-06.png)

After everything is over you should add created level to Levels fields of Manager Controller in Initializer scene.


# Create Weapon #

You can create weapon in easly. Steps:

### 1 - Create New Level Weapon ###
![alt tag](https://i.ibb.co/SwZCG9D/Screen-Shot-2021-05-01-at-23-56-22.png)

### 2 - Fill Fields of Weapon ###
![alt tag](https://i.ibb.co/xDqSjr3/Screen-Shot-2021-05-02-at-00-11-56.png)

    - Id : This field is should be uniqe and in order.
    - Damge : This field is damage amount of the weapon.
    - Capacity : This field is max capacity amount of the weapon.
    - Reload Time : This field is reloading time of the weapon.
    - Trigger Delay : This field is trigger delay of the weapon.
    - Prefab : This field is reference game object of the weapon.
    - Icon : This field is icon of the weapon.
    - Fire Clip : This field is fire sound of the weapon.
    - Reload Clip : This field is reload sound of the weapon.

### 3 - Last Step ###
![alt tag](https://i.ibb.co/mH5D2Fn/Screen-Shot-2021-05-01-at-23-40-06.png)

After everything is over you should add created weapon to Weapons fields of Manager Controller in Initializer scene.


# Sound Manager #
![alt tag](https://i.ibb.co/yS4wdxs/Screen-Shot-2021-05-02-at-00-58-15.png)

If you want change game sounds you can make with Sound Manager script. Sound Manager script is located on Game scene.


# Footages #

![alt tag](https://i.ibb.co/djWpZ9Z/Screen-Shot-2021-05-02-at-00-51-37.png)
![alt tag](https://i.ibb.co/BfZ9TpJ/Screen-Shot-2021-05-02-at-00-51-45.png)
![alt tag](https://i.ibb.co/9tF0dZ5/Screen-Shot-2021-05-02-at-00-51-55.png)
![alt tag](https://i.ibb.co/6WsCgG8/Screen-Shot-2021-05-02-at-00-52-18.png)
