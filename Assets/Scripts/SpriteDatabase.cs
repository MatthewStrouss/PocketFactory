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
        }

        private void RegisterResourceSprites()
        {
            //Sprite[] resourceSprites = UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(@"Assets\productsSpriteSheet.png") as Sprite[];
            object[] resourceSprites = UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(@"Assets/productsSpriteSheet.png");
            
            RegisterSprite("Resource", "(None)", UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(@"Assets/(None).png")[0] as Sprite);
            RegisterSprite("Resource", "Circuit", resourceSprites[0] as Sprite);
            RegisterSprite("Resource", "Engine", resourceSprites[1] as Sprite);
            RegisterSprite("Resource", "Heater Plate", resourceSprites[2] as Sprite);
            RegisterSprite("Resource", "Cooler Plate", resourceSprites[3] as Sprite);
            RegisterSprite("Resource", "Light Bulb", resourceSprites[4] as Sprite);
            RegisterSprite("Resource", "Clock", resourceSprites[5] as Sprite);
            RegisterSprite("Resource", "Antenna", resourceSprites[6] as Sprite);
            RegisterSprite("Resource", "Battery", resourceSprites[7] as Sprite);
            RegisterSprite("Resource", "Processor", resourceSprites[8] as Sprite);
            RegisterSprite("Resource", "Power Supply", resourceSprites[9] as Sprite);

            RegisterSprite("Resource", "Server Rack", resourceSprites[10] as Sprite);
            RegisterSprite("Resource", "Computer", resourceSprites[11] as Sprite);
            RegisterSprite("Resource", "Generator", resourceSprites[12] as Sprite);
            RegisterSprite("Resource", "Iron", resourceSprites[13] as Sprite);
            RegisterSprite("Resource", "Aluminum", resourceSprites[14] as Sprite);
            RegisterSprite("Resource", "Gold", resourceSprites[15] as Sprite);
            RegisterSprite("Resource", "Copper", resourceSprites[16] as Sprite);
            RegisterSprite("Resource", "Diamond", resourceSprites[17] as Sprite);
            RegisterSprite("Resource", "Advanced Engine", resourceSprites[18] as Sprite);
            RegisterSprite("Resource", "Electric Generator", resourceSprites[19] as Sprite);

            RegisterSprite("Resource", "Super Computer", resourceSprites[20] as Sprite);
            RegisterSprite("Resource", "Electric Engine", resourceSprites[21] as Sprite);
            RegisterSprite("Resource", "AI Processor", resourceSprites[22] as Sprite);
            RegisterSprite("Resource", "Grill", resourceSprites[23] as Sprite);
            RegisterSprite("Resource", "Toaster", resourceSprites[24] as Sprite);
            RegisterSprite("Resource", "Air Conditioner", resourceSprites[25] as Sprite);
            RegisterSprite("Resource", "Washing Machine", resourceSprites[26] as Sprite);
            RegisterSprite("Resource", "Solar Panel", resourceSprites[27] as Sprite);
            RegisterSprite("Resource", "Headphones", resourceSprites[28] as Sprite);
            RegisterSprite("Resource", "Drill", resourceSprites[29] as Sprite);

            RegisterSprite("Resource", "Speaker", resourceSprites[30] as Sprite);
            RegisterSprite("Resource", "Radio", resourceSprites[31] as Sprite);
            RegisterSprite("Resource", "Jackhammer", resourceSprites[32] as Sprite);
            RegisterSprite("Resource", "TV", resourceSprites[33] as Sprite);
            RegisterSprite("Resource", "Smartphone", resourceSprites[34] as Sprite);
            RegisterSprite("Resource", "Fridge", resourceSprites[35] as Sprite);
            RegisterSprite("Resource", "Tablet", resourceSprites[36] as Sprite);
            RegisterSprite("Resource", "Microwave", resourceSprites[37] as Sprite);
            RegisterSprite("Resource", "Railway", resourceSprites[38] as Sprite);
            RegisterSprite("Resource", "Smartwatch", resourceSprites[39] as Sprite);

            RegisterSprite("Resource", "Water Heater", resourceSprites[40] as Sprite);
            RegisterSprite("Resource", "Iron Plate", resourceSprites[41] as Sprite);
            RegisterSprite("Resource", "Aluminum Plate", resourceSprites[42] as Sprite);
            RegisterSprite("Resource", "Copper Plate", resourceSprites[43] as Sprite);
            RegisterSprite("Resource", "Gold Plate", resourceSprites[44] as Sprite);
            RegisterSprite("Resource", "Diamond Plate", resourceSprites[45] as Sprite);
            RegisterSprite("Resource", "Iron Wire", resourceSprites[46] as Sprite);
            RegisterSprite("Resource", "Aluminum Wire", resourceSprites[47] as Sprite);
            RegisterSprite("Resource", "Copper Wire", resourceSprites[48] as Sprite);
            RegisterSprite("Resource", "Gold Wire", resourceSprites[49] as Sprite);

            RegisterSprite("Resource", "Diamond Wire", resourceSprites[50] as Sprite);
            RegisterSprite("Resource", "Iron Gear", resourceSprites[51] as Sprite);
            RegisterSprite("Resource", "Aluminum Gear", resourceSprites[52] as Sprite);
            RegisterSprite("Resource", "Copper Gear", resourceSprites[53] as Sprite);
            RegisterSprite("Resource", "Gold Gear", resourceSprites[54] as Sprite);
            RegisterSprite("Resource", "Diamond Gear", resourceSprites[55] as Sprite);
            RegisterSprite("Resource", "Iron Liquid", resourceSprites[56] as Sprite);
            RegisterSprite("Resource", "Aluminum Liquid", resourceSprites[57] as Sprite);
            RegisterSprite("Resource", "Copper Liquid", resourceSprites[58] as Sprite);
            RegisterSprite("Resource", "Gold Liquid", resourceSprites[59] as Sprite);

            RegisterSprite("Resource", "Diamond Liquid", resourceSprites[60] as Sprite);
            RegisterSprite("Resource", "Drone", resourceSprites[61] as Sprite);
            RegisterSprite("Resource", "Electric Board", resourceSprites[62] as Sprite);
            RegisterSprite("Resource", "Stove", resourceSprites[63] as Sprite);
            RegisterSprite("Resource", "Lazer", resourceSprites[64] as Sprite);
            RegisterSprite("Resource", "AI Robot Body", resourceSprites[65] as Sprite);
            RegisterSprite("Resource", "AI Robot Head", resourceSprites[66] as Sprite);
            RegisterSprite("Resource", "AI Robot", resourceSprites[67] as Sprite);
        }

        private void RegisterMachineSprites()
        {
            //Sprite[] machineSprites = UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(@"Assets\MachineSprites.png") as Sprite[];
            object[] machineSprites = UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(@"Assets\MachineSprites.png");
            RegisterSprite("Machine", "Crafter", machineSprites[0] as Sprite);
            RegisterSprite("Machine", "Cutter", machineSprites[1] as Sprite);
            RegisterSprite("Machine", "Furnace", machineSprites[2] as Sprite);
            RegisterSprite("Machine", "Right Selector", machineSprites[3] as Sprite);
            RegisterSprite("Machine", "Right Splitter", machineSprites[4] as Sprite);
            RegisterSprite("Machine", "Left Selector", machineSprites[5] as Sprite);
            RegisterSprite("Machine", "Left Splitter", machineSprites[6] as Sprite);
            RegisterSprite("Machine", "Roller", machineSprites[7] as Sprite);
            RegisterSprite("Machine", "Multi Selector", machineSprites[8] as Sprite);
            RegisterSprite("Machine", "Splitter", machineSprites[9] as Sprite);

            RegisterSprite("Machine", "3-way Splitter", machineSprites[10] as Sprite);
            RegisterSprite("Machine", "Hydraulic Press", machineSprites[11] as Sprite);
            RegisterSprite("Machine", "Transporter Input", machineSprites[12] as Sprite);
            RegisterSprite("Machine", "Transporter Output", machineSprites[13] as Sprite);
            RegisterSprite("Machine", "Robotic Arm", machineSprites[14] as Sprite);
            RegisterSprite("Machine", "Seller", machineSprites[15] as Sprite);
            RegisterSprite("Machine", "Starter", machineSprites[16] as Sprite);
            RegisterSprite("Machine", "Filter Robot Arm", machineSprites[17] as Sprite);
            RegisterSprite("Machine", "Wire Drawer", machineSprites[18] as Sprite);
            RegisterSprite("Machine", "Timed Roller", machineSprites[19] as Sprite);
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
