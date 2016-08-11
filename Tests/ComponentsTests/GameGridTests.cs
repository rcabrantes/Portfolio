using ColorWarsMultiplayerActors.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ComponentsTests
{
    [TestFixture]
    public class GameGridTests
    {
        private GameGrid grid;


        [SetUp]
        public void Setup()
        {
            grid = new GameGrid(100, 50);


        }

        [Test]
        public void WhenGridCreated_SetsCountVariables()
        {

            Assert.AreEqual(100, grid.HorizontalCount);
            Assert.AreEqual(50, grid.VerticalCount);

        }

        [Test]
        public void WhenGridCreated_DimensionsGrid()
        {

            Assert.AreEqual(99, grid.Grid.GetUpperBound(0));

            Assert.AreEqual(49, grid.Grid.GetUpperBound(1));

        }


        [Test]
        public void WhenGridCreated_InitializesGrid()
        {

            foreach(var cell in grid.Grid)
            {
                Assert.AreEqual(0, cell.Owner);
            }

        }

        [TestCase(0,0)]
        [TestCase(20, 20)]
        [TestCase(50, 12)]
        [TestCase(29, 21)]
        public void SettingPlayerInitialCell_SetsCellOwner(int x, int y)
        {

            grid.SetPlayerInitialCell(1, x, y);

            Assert.AreEqual(1, grid.Grid[x, y].Owner);

        }


        private void SetupPlayableGrid()
        {
            grid = new GameGrid(3, 3);

            foreach (var cell in grid.Grid)
            {
                cell.Color = 0;
            }

            //Setup a path of cells that must be captured.
            grid.Grid[0, 0].Color = 1;
            grid.Grid[1, 1].Color = 1;
            grid.Grid[2, 1].Color = 1;
            grid.Grid[2, 2].Color = 1;
            grid.Grid[0, 2].Color = 1;

            grid.Grid[2, 0].Color = 2;
            grid.Grid[1, 2].Color = 2;

        }

        [Test]
        public void SettingPlayerInitialCell_CapturesSameColorNeighbors()
        {
            SetupPlayableGrid();

            grid.SetPlayerInitialCell(1, 0, 0);

            //All color 1 cells that have a path to 0,0 should be captured

            Assert.AreEqual(1, grid.Grid[1, 1].Owner);
            Assert.AreEqual(1, grid.Grid[2, 1].Owner);
            Assert.AreEqual(1, grid.Grid[2, 2].Owner);
            Assert.AreEqual(0, grid.Grid[0, 2].Owner);

        }


        [Test]
        public void SettingPlayerInitialCell_WithHomogenousGrid_CapturesEveryCell()
        {
            grid = new GameGrid(20, 20);

            foreach (var cell in grid.Grid)
            {
                cell.Color = 2;
            }  

            grid.SetPlayerInitialCell(1, 0, 0);


            foreach(var cell in grid.Grid)
            {
                Assert.AreEqual(1, cell.Owner);
            }

        }

        [Test]
        public void WhenPlayerPlays_ChangesHisCellsColors()
        {
            SetupPlayableGrid();

            grid.SetPlayerInitialCell(1, 0, 0);

            grid.Play(1, 4);

            Assert.AreEqual(4, grid.Grid[0, 0].Color);
            Assert.AreEqual(4, grid.Grid[1, 1].Color);
            Assert.AreEqual(4, grid.Grid[2, 1].Color);
            Assert.AreEqual(4, grid.Grid[2, 2].Color);
        }

        [Test]
        public void WhenPlayerPlays_DoesntChangeUnownedCells()
        {
            SetupPlayableGrid();

            grid.SetPlayerInitialCell(1, 0, 0);

            grid.Play(1, 4);

            Assert.AreEqual(0, grid.Grid[1, 0].Owner);
            Assert.AreEqual(0, grid.Grid[2, 0].Owner);
            Assert.AreEqual(0, grid.Grid[1, 2].Owner);
            Assert.AreEqual(0, grid.Grid[0, 1].Owner);
        }

        [Test]
        public void WhenPlayerPlays_CapturesAdjacentCells()
        {
            SetupPlayableGrid();

            grid.SetPlayerInitialCell(1, 0, 0);

            grid.Play(1, 2);

            Assert.AreEqual(1, grid.Grid[2, 0].Owner);
            Assert.AreEqual(1, grid.Grid[1, 2].Owner);
            
        }

        [Test]
        public void WhenSettingInitialPositions_WithTwoPlayers_ChoosesOpposingCorners()
        {
            SetupPlayableGrid();

            grid.SetInitialPositions(2);

            Assert.AreEqual(1, grid.Grid[0, 0].Owner);
            Assert.AreEqual(2, grid.Grid[2, 2].Owner);

        }
    }
}
