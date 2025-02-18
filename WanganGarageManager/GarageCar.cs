﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace WanganGarageManager
{
    public class GarageCar
    {
        string filename;
        byte[] data;

        public bool inEditor = false;
        public bool hasSaved = true;
        public bool isLoaded = false;
        public bool confirmedDiscard = false;

        public ushort ver;

        public byte rearWing;
        public byte sideMirrors;
        public byte bodySticker;
        public byte japanSticker;
        public byte stickerColour;
        public byte carbonTrunk;
        public byte numberPlateFrame;
        public byte specialPlateFrame;
        public byte unk1;
        public byte unk2;
        public byte plateFrameColour;
        public ushort numberPlateNumber;
        public byte bodyKit;
        public byte hood;
        public byte carColour;
        public byte customColour;
        public byte wheels;
        public byte wheelColour;
        public byte neons;
        public byte numberPlatePrefecture;
        public byte car;

        public byte power;
        public byte handling;
        public byte rank;

        public GarageCar(string filename)
        {
            this.filename = filename;
        }

        public void LoadCar()
        {
            data = File.ReadAllBytes(filename);

            ver = BitConverter.ToUInt16(data, 0x00);

            numberPlatePrefecture = data[0x20];
            car = data[0x2C];

            carColour = data[0x30];
            customColour = data[0x34];
            wheels = data[0x38];
            wheelColour = data[0x3C];
            neons = data[0x7C];

            bodyKit = data[0x40];
            hood = data[0x44];

            rearWing = data[0x50];
            sideMirrors = data[0x54];
            bodySticker = data[0x58];
            japanSticker = data[0x59];
            stickerColour = data[0x5C];

            carbonTrunk = data[0x80];
            numberPlateFrame = data[0x84];
            specialPlateFrame = data[0x85];
            unk1 = data[0x86];
            unk2 = data[0x87];
            plateFrameColour = data[0x8A];
            numberPlateNumber = BitConverter.ToUInt16(data, 0x8C);

            power = data[0x98];
            handling = data[0x9C];

            rank = data[0xA4];

            Console.WriteLine("State of save: rearWing={0}, sideMirrors={1}, bodySticker={2}, japanSticker={3}, stickerColour={4}, carbonTrunk={5}, numberPlateFrame={6}" +
                ", specialPlateFrame={7}, unk1={8}, unk2={9}, plateFrameColour={10}, numberPlateNumber={11}, bodyKit={12}, hood={13}" +
                ", carColour={14}, wheels={15}, wheelColour={16}, numberPlatePrefecture={17}, car={18}, rank={19} "
                , rearWing, sideMirrors, bodySticker, japanSticker, stickerColour, carbonTrunk, numberPlateFrame, specialPlateFrame, unk1, unk2, plateFrameColour
                , numberPlateNumber, bodyKit, hood, carColour, wheels, wheelColour, numberPlatePrefecture, car, rank);
            isLoaded = true;
        }

        public void SaveCar()
        {
            data[0x20] = numberPlatePrefecture;
            data[0x2C] = car;

            data[0x30] = carColour;
            data[0x34] = customColour;
            data[0x38] = wheels;
            data[0x3C] = wheelColour;
            data[0x7C] = neons;

            data[0x40] = bodyKit;
            data[0x44] = hood;

            data[0x50] = rearWing;
            data[0x54] = sideMirrors;
            data[0x58] = bodySticker;
            data[0x59] = japanSticker;
            data[0x5C] = stickerColour;

            data[0x80] = carbonTrunk;
            data[0x84] = numberPlateFrame;
            data[0x85] = specialPlateFrame;
            data[0x86] = unk1;
            data[0x87] = unk2;
            data[0x8A] = plateFrameColour;
            InsertBytesIntoBuffer(data, BitConverter.GetBytes(numberPlateNumber), 0x8C);

            data[0x98] = power;
            data[0x9C] = handling;

            data[0xA4] = rank;

            File.WriteAllBytes(filename, data);
            hasSaved = true;
        }

        public void DeleteCar()
        {
            File.Delete(filename);
            Console.WriteLine("Deleted {0}", filename);
        }

        public void FCSCar()
        {
            string newFilename = @".\OpenParrot_Cars\custom.car";
            File.Copy(filename, newFilename);
        }

        public void FCSOFFCar()
        {
            File.Delete(@".\OpenParrot_Cars\custom.car");
            Console.WriteLine("Deleted {0}" + "custom.car");
        }

        public void UpdateAeroKit(int value)
        {
            if (bodyKit != value)
            {
                bodyKit = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateWing(int value)
        {
            if (rearWing != CarDB.wing[value])
            {
                rearWing = CarDB.wing[(byte)value];
                hasSaved = false;
            }
        }

        public void UpdateRims(int value)
        {
            if (wheels != CarDB.rims[value])
            {
                wheels = CarDB.rims[(byte)value];
                hasSaved = false;
            }
        }

        public void UpdateRimsColour(int value)
        {
            if (wheelColour != value)
            {
                wheelColour = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateStickers(int value)
        {
            if (bodySticker != CarDB.stickers[value])
            {
                bodySticker = CarDB.stickers[(byte)value];
                hasSaved = false;
            }
        }

        public void UpdateStickerColour(int value)
        {
            if (stickerColour != value)
            {
                stickerColour = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateTrunk(int value)
        {
            if (carbonTrunk != value)
            {
                carbonTrunk = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateMirrors(int value)
        {
            if (sideMirrors != value)
            {
                sideMirrors = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateHood(int value)
        {
            if (hood != value)
            {
                hood = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdatePlateFrame(int value)
        {
            if (numberPlateFrame != value)
            {
                numberPlateFrame = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdatePlateFrameColour(int value)
        {
            if (plateFrameColour != value)
            {
                plateFrameColour = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateNeons(int value)
        {
            if (neons != value)
            {
                neons = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateCarColour(int value)
        {
            if (carColour != value)
            {
                carColour = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdateCustomColour(int value)
        {
            if (customColour != value)
            {
                customColour = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdatePlate(ushort value)
        {
            if (numberPlateNumber != value)
            {
                numberPlateNumber = value;
                hasSaved = false;
            }
        }

        public void UpdatePlatePrefecture(int value)
        {
            if (numberPlatePrefecture != value)
            {
                numberPlatePrefecture = (byte)value;
                hasSaved = false;
            }
        }

        public void Updatelevel(int value)
        {
            if (rank != value)
            {
                rank = (byte)value;
                hasSaved = false;
            }
        }

        public void UpdatePowerHandling(int value1, int value2)
        {
            if (power != value1 || handling != value2)
            {
                power = (byte)value1;
                handling = (byte)value2;
                hasSaved = false;
            }
        }

        public ListViewItem GetListViewItem(ImageList imgLst)
        {
            ListViewItem item = new ListViewItem();
            item.Name = Path.GetFileNameWithoutExtension(filename);
            item.Text = CarDB.GetCarName(car);
            if (Properties.Settings.Default.usePowerHandling)
            {
                item.SubItems.Add("P:" + power.ToString() + "/H:" + handling.ToString());
            }
            else
            {
                item.SubItems.Add(CarDB.power[power]);
            }
            item.SubItems.Add(Path.GetFileName(filename));
            item.ImageIndex = imgLst.Images.IndexOfKey(GetPreviewImageName(car));
            return item;
        }

        public void UpdateListViews(frmEditor editor)
        {
            editor.lstAero.SelectedIndices.Add(bodyKit);
            editor.lstWing.SelectedIndices.Add(Array.IndexOf(CarDB.wing, rearWing));
            editor.lstRims.SelectedIndices.Add(Array.IndexOf(CarDB.rims, wheels));
            editor.lstStickers.SelectedIndices.Add(Array.IndexOf(CarDB.stickers, bodySticker));
            editor.lstCustomColour.SelectedIndices.Add(customColour);
            editor.lstTrunk.SelectedIndices.Add(carbonTrunk);
            editor.lstMirror.SelectedIndices.Add(sideMirrors);
            editor.lstHood.SelectedIndices.Add(hood);
            editor.lstPlateFrame.SelectedIndices.Add(numberPlateFrame);
            editor.lstNeons.SelectedIndices.Add(neons);

            editor.trkPower.Value = power;
            editor.trkHandling.Value = handling;

            string number = numberPlateNumber.ToString().PadLeft(4, '0');
            editor.txtNum1.Text = new string(number.Take(2).ToArray());
            editor.txtNum2.Text = new string(number.Skip(2).ToArray());

            editor.cmbPrefecture.SelectedIndex = numberPlatePrefecture;
            editor.cmdlevel.SelectedIndex = rank;
            //editor.Rankimg.Image = 

            Application.DoEvents();
            hasSaved = true;
        }

        public void UpdateColourPanelRims(Button[] btns)
        {
            int count = 0;
            for (int i = 0; i < CarDB.rimColours[wheels].Length; i++)
            {
                btns[i].Visible = true;
                btns[i].BackColor = CarDB.rimColours[wheels][i];
                btns[i].Text = "";
                count++;
            }
            for (int i = count; i < btns.Length; i++)
            {
                btns[i].Visible = false;
            }
            btns[wheelColour].Focus();
        }

        public void UpdateColourPanelPlateFrame(Button[] btns)
        {
            int count = 0;
            for (int i = 0; i < CarDB.plateFrameColours[numberPlateFrame].Length; i++)
            {
                btns[i].Visible = true;
                btns[i].BackColor = CarDB.plateFrameColours[numberPlateFrame][i];
                if (numberPlateFrame == 3)
                {
                    btns[i].Text = CarDB.ymSpecialNames[i];
                    if (btns[i].BackColor == Color.Black) { btns[i].ForeColor = Color.White; }
                    else { btns[i].ForeColor = Color.Black; }
                } else { btns[i].Text = ""; }
                count++;
            }
            for (int i = count; i < btns.Length; i++)
            {
                btns[i].Visible = false;
            }
            btns[plateFrameColour].Focus();
        }

        public void UpdateColourPanelStickers(Button[] btns)
        {
            int count = 0;
            for (int i = 0; i < CarDB.stickerColours[bodySticker].Length; i++)
            {
                btns[i].Visible = true;
                btns[i].BackColor = CarDB.stickerColours[bodySticker][i];
                btns[i].Text = "";
                count++;
            }
            for (int i = count; i < btns.Length; i++)
            {
                btns[i].Visible = false;
            }
            btns[stickerColour].Focus();
        }

        public void UpdateColourPanelColour(Button[] btns)
        {
            int count = 0;
            for (int i = 0; i < CarDB.carColours[car].Length; i++)
            {
                btns[i].Visible = true;
                btns[i].BackColor = CarDB.carColours[car][i];
                btns[i].Text = "";
                count++;
            }
            for (int i = count; i < btns.Length; i++)
            {
                btns[i].Visible = false;
            }
            btns[carColour].Focus();
        }

        public string GetPreviewImageName(byte id)
        {
            if (CarDB.ContainsCar(id))
            {
                return id.ToString("X2") + ".png";
            }
            else
            {
                return "FF.png";
            }
        }

        public void InsertBytesIntoBuffer(byte[] buf, byte[] bufToInsert, int startIndex)
        {
            for (int i = startIndex; i < startIndex + bufToInsert.Length; i++)
            {
                buf[i] = bufToInsert[i - startIndex];
            }
        }
    }
}
