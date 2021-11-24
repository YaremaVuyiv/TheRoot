using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Components
{
    public partial class MarquiseTabletComponent: IDisposable
    {
        [Inject]
        public StateContainer StateContainer { get; init; }

        private List<Slot> SawmillSlots => GetSawmills();
        private List<Slot> WorkShopSlots => GetWorkShops();
        private List<Slot> RecruiterSlots => GetRecruiters();
        //private int WarriorsCount => 25 - StateContainer.State.Warriors.Where(x => x.Faction == FactionType.MarquiseDeCat).Count();

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }

        protected override void OnInitialized()
        {
            /*SawmillSlots = new List<Slot>
            {
                new Slot(new System.Drawing.Point(48, 39), 8),
                new Slot(new System.Drawing.Point(56, 39), 8),
                new Slot(new System.Drawing.Point(64, 39), 8),
                new Slot(new System.Drawing.Point(71, 39), 8),
                new Slot(new System.Drawing.Point(79, 39), 8),
                new Slot(new System.Drawing.Point(87, 39), 8)
            };

            RecruiterSlots = new List<Slot>
            {
                new Slot(new System.Drawing.Point(48, 59), 8),
                new Slot(new System.Drawing.Point(56, 59), 8),
                new Slot(new System.Drawing.Point(64, 59), 8),
                new Slot(new System.Drawing.Point(71, 59), 8),
                new Slot(new System.Drawing.Point(79, 59), 8),
                new Slot(new System.Drawing.Point(87, 59), 8)
            };

            WorkShopSlots = new List<Slot>
            {
                new Slot(new System.Drawing.Point(48, 49), 8),
                new Slot(new System.Drawing.Point(56, 49), 8),
                new Slot(new System.Drawing.Point(64, 49), 8),
                new Slot(new System.Drawing.Point(71, 49), 8),
                new Slot(new System.Drawing.Point(79, 49), 8),
                new Slot(new System.Drawing.Point(87, 49), 8)
            };

            var sawmillsCount = 0;
            var recruitersCount = 0;
            var workShopsCount = 0;
            foreach (var c in StateContainer.State)
            {
                sawmillsCount += c.Slots.Where(x => x.SlotPiece is SawmillBuilding).Count();
                recruitersCount += c.Slots.Where(x => x.SlotPiece is RecruiterBuilding).Count();
                workShopsCount += c.Slots.Where(x => x.SlotPiece is WorkShopBuilding).Count();
            }

            for (int i = 5; i >= sawmillsCount; i--)
            {
                SawmillSlots[i].SlotPiece = new SawmillBuilding();
            }

            for (int i = 5; i >= recruitersCount; i--)
            {
                RecruiterSlots[i].SlotPiece = new RecruiterBuilding();
            }

            for (int i = 5; i >= workShopsCount; i--)
            {
                WorkShopSlots[i].SlotPiece = new WorkShopBuilding();
            }*/

            StateContainer.OnChange += StateHasChanged;
        }

        private List<Slot> GetRecruiters()
        {
            var recruitersCount = 0;
            /*foreach (var c in StateContainer.State.Clearings)
            {
                recruitersCount += c.Slots.Where(x => x.SlotPiece is RecruiterBuilding).Count();
            }*/

            var recruiters = new List<Slot>
            {
                new Slot(new System.Drawing.Point(48, 59), 8),
                new Slot(new System.Drawing.Point(56, 59), 8),
                new Slot(new System.Drawing.Point(64, 59), 8),
                new Slot(new System.Drawing.Point(71, 59), 8),
                new Slot(new System.Drawing.Point(79, 59), 8),
                new Slot(new System.Drawing.Point(87, 59), 8)
            };

            /*for (int i = 5; i >= recruitersCount; i--)
            {
                recruiters[i].SlotPiece = new RecruiterBuilding();
            }*/

            return recruiters;
        }

        private List<Slot> GetSawmills()
        {
            var sawmillsCount = 0;
            /*foreach (var c in StateContainer.State.Clearings)
            {
                sawmillsCount += c.Slots.Where(x => x.SlotPiece is SawmillBuilding).Count();
            }*/

            var sawmills = new List<Slot>
            {
                new Slot(new System.Drawing.Point(48, 39), 8),
                new Slot(new System.Drawing.Point(56, 39), 8),
                new Slot(new System.Drawing.Point(64, 39), 8),
                new Slot(new System.Drawing.Point(71, 39), 8),
                new Slot(new System.Drawing.Point(79, 39), 8),
                new Slot(new System.Drawing.Point(87, 39), 8)
            };

            /*for (int i = 5; i >= sawmillsCount; i--)
            {
                sawmills[i].SlotPiece = new SawmillBuilding();
            }*/

            return sawmills;
        }

        private List<Slot> GetWorkShops()
        {
            var workShopsCount = 0;
            //var workShopsCount = StateContainer.State.Clearings.Sum(x => x.Slots.Where(s => s.SlotPiece is WorkShopBuilding).Count());

            var workShops = new List<Slot>
            {
                new Slot(new System.Drawing.Point(48, 49), 8),
                new Slot(new System.Drawing.Point(56, 49), 8),
                new Slot(new System.Drawing.Point(64, 49), 8),
                new Slot(new System.Drawing.Point(71, 49), 8),
                new Slot(new System.Drawing.Point(79, 49), 8),
                new Slot(new System.Drawing.Point(87, 49), 8)
            };

            /*for (int i = 5; i >= workShopsCount; i--)
            {
                workShops[i].SlotPiece = new FactionPiece(FactionType.MarquiseDeCat, "test.png", PieceType.Building);
            }*/

            return workShops;
        }
    }
}
