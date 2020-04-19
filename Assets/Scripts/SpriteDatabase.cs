using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class SpriteDatabase
    {
        private static SpriteDatabase instance;

        private Dictionary<string, Dictionary<string, Sprite>> sprites = new Dictionary<string, Dictionary<string, Sprite>>(StringComparer.InvariantCultureIgnoreCase);

        public static SpriteDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpriteDatabase();
                }

                return instance;
            }
        }

        public SpriteDatabase()
        {
            RegisterResourceSprites();
            RegisterMachineSprites();
            RegisterUISprites();
        }

        private void RegisterResourceSprites()
        {
            //Sprite[] resourceSprites = UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(@"Assets\productsSpriteSheet.png") as Sprite[];
            Sprite[] resourceSprites = Resources.LoadAll<Sprite>(@"productsSpriteSheet");
            
            RegisterSprite("Resource", "(None)", Resources.LoadAll<Sprite>(@"(None)").FirstOrDefault() as Sprite);
            RegisterSprite("Resource", "Circuit", resourceSprites[0]);
            RegisterSprite("Resource", "Engine", resourceSprites[1]);
            RegisterSprite("Resource", "Heater Plate", resourceSprites[2]);
            RegisterSprite("Resource", "Cooler Plate", resourceSprites[3]);
            RegisterSprite("Resource", "Light Bulb", resourceSprites[4]);
            RegisterSprite("Resource", "Clock", resourceSprites[5]);
            RegisterSprite("Resource", "Antenna", resourceSprites[6]);
            RegisterSprite("Resource", "Battery", resourceSprites[7]);
            RegisterSprite("Resource", "Processor", resourceSprites[8]);
            RegisterSprite("Resource", "Power Supply", resourceSprites[9]);

            RegisterSprite("Resource", "Server Rack", resourceSprites[10]);
            RegisterSprite("Resource", "Computer", resourceSprites[11]);
            RegisterSprite("Resource", "Generator", resourceSprites[12]);
            RegisterSprite("Resource", "Iron", resourceSprites[13]);
            //RegisterSprite("Resource", "Aluminum", resourceSprites[14]);
            RegisterSprite("Resource", "Aluminum", Resources.LoadAll<Sprite>(@"16-raw-aluminium").FirstOrDefault() as Sprite);
            RegisterSprite("Resource", "Gold", resourceSprites[15]);
            RegisterSprite("Resource", "Copper", resourceSprites[16]);
            RegisterSprite("Resource", "Diamond", resourceSprites[17]);
            RegisterSprite("Resource", "Advanced Engine", resourceSprites[18]);
            RegisterSprite("Resource", "Electric Generator", resourceSprites[19]);

            RegisterSprite("Resource", "Super Computer", resourceSprites[20]);
            RegisterSprite("Resource", "Electric Engine", resourceSprites[21]);
            RegisterSprite("Resource", "AI Processor", resourceSprites[22]);
            RegisterSprite("Resource", "Grill", resourceSprites[23]);
            RegisterSprite("Resource", "Toaster", resourceSprites[24]);
            RegisterSprite("Resource", "Air Conditioner", resourceSprites[25]);
            RegisterSprite("Resource", "Washing Machine", resourceSprites[26]);
            RegisterSprite("Resource", "Solar Panel", resourceSprites[27]);
            RegisterSprite("Resource", "Headphones", resourceSprites[28]);
            RegisterSprite("Resource", "Drill", resourceSprites[29]);

            RegisterSprite("Resource", "Speaker", resourceSprites[30]);
            RegisterSprite("Resource", "Radio", resourceSprites[31]);
            RegisterSprite("Resource", "Jackhammer", resourceSprites[32]);
            RegisterSprite("Resource", "TV", resourceSprites[33]);
            RegisterSprite("Resource", "Smartphone", resourceSprites[34]);
            RegisterSprite("Resource", "Fridge", resourceSprites[35]);
            RegisterSprite("Resource", "Tablet", resourceSprites[36]);
            RegisterSprite("Resource", "Microwave", resourceSprites[37]);
            RegisterSprite("Resource", "Railway", resourceSprites[38]);
            RegisterSprite("Resource", "Smartwatch", resourceSprites[39]);

            RegisterSprite("Resource", "Water Heater", resourceSprites[40]);
            RegisterSprite("Resource", "Iron Plate", resourceSprites[41]);
            RegisterSprite("Resource", "Aluminum Plate", resourceSprites[42]);
            RegisterSprite("Resource", "Copper Plate", resourceSprites[43]);
            RegisterSprite("Resource", "Gold Plate", resourceSprites[44]);
            RegisterSprite("Resource", "Diamond Plate", resourceSprites[45]);
            RegisterSprite("Resource", "Iron Wire", resourceSprites[46]);
            RegisterSprite("Resource", "Aluminum Wire", resourceSprites[47]);
            RegisterSprite("Resource", "Copper Wire", resourceSprites[48]);
            RegisterSprite("Resource", "Gold Wire", resourceSprites[49]);

            RegisterSprite("Resource", "Diamond Wire", resourceSprites[50]);
            RegisterSprite("Resource", "Iron Gear", resourceSprites[51]);
            RegisterSprite("Resource", "Aluminum Gear", resourceSprites[52]);
            RegisterSprite("Resource", "Copper Gear", resourceSprites[53]);
            RegisterSprite("Resource", "Gold Gear", resourceSprites[54]);
            RegisterSprite("Resource", "Diamond Gear", resourceSprites[55]);
            RegisterSprite("Resource", "Iron Liquid", resourceSprites[56]);
            RegisterSprite("Resource", "Aluminum Liquid", resourceSprites[57]);
            RegisterSprite("Resource", "Copper Liquid", resourceSprites[58]);
            RegisterSprite("Resource", "Gold Liquid", resourceSprites[59]);

            RegisterSprite("Resource", "Diamond Liquid", resourceSprites[60]);
            RegisterSprite("Resource", "Drone", resourceSprites[61]);
            RegisterSprite("Resource", "Electric Board", resourceSprites[62]);
            RegisterSprite("Resource", "Stove", resourceSprites[63]);
            RegisterSprite("Resource", "Lazer", resourceSprites[64]);
            RegisterSprite("Resource", "AI Robot Body", resourceSprites[65]);
            RegisterSprite("Resource", "AI Robot Head", resourceSprites[66]);
            RegisterSprite("Resource", "AI Robot", resourceSprites[67]);
        }

        private void RegisterMachineSprites()
        {
            //Sprite[] machineSprites = UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(@"Assets\MachineSprites.png") as Sprite[];
            Sprite[] machineSprites = Resources.LoadAll<Sprite>(@"MachineSprites");
            RegisterSprite("Machine", "Crafter", machineSprites[0]);
            RegisterSprite("Machine", "Cutter", machineSprites[1]);
            RegisterSprite("Machine", "Furnace", machineSprites[2]);
            RegisterSprite("Machine", "Right Selector", machineSprites[3]);
            RegisterSprite("Machine", "Right Splitter", machineSprites[4]);
            RegisterSprite("Machine", "Left Selector", machineSprites[5]);
            RegisterSprite("Machine", "Left Splitter", machineSprites[6]);
            RegisterSprite("Machine", "Roller", machineSprites[7]);
            RegisterSprite("Machine", "Multi Selector", machineSprites[8]);
            RegisterSprite("Machine", "Splitter", machineSprites[9]);

            RegisterSprite("Machine", "3-way Splitter", machineSprites[10]);
            RegisterSprite("Machine", "Hydraulic Press", machineSprites[11]);
            RegisterSprite("Machine", "Transporter Input", machineSprites[12]);
            RegisterSprite("Machine", "Transporter Output", machineSprites[13]);
            RegisterSprite("Machine", "Robotic Arm", machineSprites[14]);
            RegisterSprite("Machine", "Seller", machineSprites[15]);
            RegisterSprite("Machine", "Starter", machineSprites[16]);
            RegisterSprite("Machine", "Filter Robot Arm", machineSprites[17]);
            RegisterSprite("Machine", "Wire Drawer", machineSprites[18]);
            RegisterSprite("Machine", "Timed Roller", machineSprites[19]);
        }

        private void RegisterUISprites()
        {
            RegisterSprite("UI", "Lock", Resources.Load(@"lock") as Sprite);
        }

        public void RegisterSprite(string spriteType, string spriteName, Sprite sprite)
        {
            if (sprites.TryGetValue(spriteType, out Dictionary<string, Sprite> existingSpriteType))
            {
                if (existingSpriteType.TryGetValue(spriteName, out Sprite existingSprite))
                {
                    // Error, sprite name exists
                }
                else
                {
                    existingSpriteType.Add(spriteName, sprite);
                }
            }
            else
            {
                sprites.Add(spriteType, new Dictionary<string, Sprite>(StringComparer.InvariantCultureIgnoreCase));
                sprites[spriteType].Add(spriteName, sprite);
            }
        }

        public Sprite GetSprite(string spriteType, string spriteName)
        {
            sprites.TryGetValue(spriteType, out Dictionary<string, Sprite> spriteTypeDict);
            spriteTypeDict.TryGetValue(spriteName, out Sprite sprite);
            return sprite;
        }
    }
}
