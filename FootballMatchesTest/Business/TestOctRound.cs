using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FootballLib;
using FootballLib.DataProviders;
using FootballLib.Rounds;

namespace FootballMatchesTest.Business
{
    [TestClass]
    public class TestOctRound
    {
        WorldCup worldCup;
        PreRound preRound;
        PlayOff playOff;
        OctRound octRound;
        private void Init(IProvider provider)
        {
            worldCup = new WorldCup();
            worldCup.TeamProvider = provider;

            preRound = new PreRound();
            playOff = new PlayOff();
            octRound = new OctRound();

            preRound.InputTeams = worldCup.TeamList;
            playOff.InputTeams = preRound.Play();
            octRound.InputTeams = playOff.Play();
        }
        [TestMethod]
        public void Has8TeamInput()
        {
            Init(new TextProvider());
            Assert.AreEqual(8, octRound.InputTeams.Count);
        }
        [TestMethod]
        public void Has8TeamInputWithSql()
        {
            Init(new SqlProvider());
            Assert.AreEqual(8, octRound.InputTeams.Count);
        }

        [TestMethod]
        public void Has4WonTeam()
        {
            Init(new TextProvider());
            Assert.AreEqual(4, octRound.Play().Count);
        }
        [TestMethod]
        public void Has4WonTeamWithSql()
        {
            Init(new SqlProvider());
            Assert.AreEqual(4, octRound.Play().Count);
        }
    }
}
