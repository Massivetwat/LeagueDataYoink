using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RiotSharp;
using System.Threading;



namespace leaguestuff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread getstuff = new Thread(getData) { IsBackground = true };
            getstuff.Start();

            
            
            
        }

        void getData()
        {
            try
            {
                listBox1.Items.Clear();
                var api = RiotApi.GetDevelopmentInstance("ADD YOUR OWN API HERE");
                var summoner = api.Summoner.GetSummonerByNameAsync(RiotSharp.Misc.Region.Eune, textBox1.Text).Result;
                textBox2.Text = summoner.Level.ToString(); // level of summoner
                textBox3.Text = summoner.AccountId; // accound id to textbox

                var latestVersion = "10.23.1";

                var championMasteries = api.ChampionMastery.GetChampionMasteriesAsync(RiotSharp.Misc.Region.Eune, summoner.Id).Result;

                foreach (var championMastery in championMasteries)
                {
                    var id = championMastery.ChampionId;
                    var name = api.DataDragon.Champions.GetAllAsync(latestVersion).Result.Champions.Values.Single(x => x.Id == id).Name; // using System.Linq;
                    var level = championMastery.ChampionLevel;
                    var points = championMastery.ChampionPoints;

                    listBox1.Items.Add($" •  NAME : {name} LEVEL : {level} POINTS : {points} ");
                }





            }
            catch (RiotSharpException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
