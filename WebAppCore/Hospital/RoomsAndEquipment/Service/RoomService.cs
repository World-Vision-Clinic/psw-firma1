using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using System;
using System.Collections.Generic;

namespace Hospital.RoomsAndEquipment.Service
{
    public class RoomService
    {
        private RoomRepository repository;
        private EquipmentRepository equipmentRepository;
        private EquipmentService equipmentService;
        public RoomService(RoomRepository repos)
        {
            repository = repos;
        }
        public RoomService(RoomRepository repos, EquipmentRepository equipRepo)
        {
            repository = repos;
            equipmentRepository = equipRepo;
            equipmentService = new EquipmentService(equipRepo);
        }
        public List<Room> getAll()
        {
            return repository.GetAll();
        }

        public List<Room> getRoomsForFloor(int id)
        {
            return repository.GetRoomsForFloor(id);
        }

        public List<Room> getRoomsForFloorAndBuilding(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public Room GetById(int id)
        {
            return repository.GetByID(id);
        }

        public bool RoomExists(int id)
        {
            return repository.Exists(id);
        }

        public void Update(Room room)
        {
            repository.Update(room);
        }

        public List<int> getRoomIdsForBuilding(int buildingId, FloorService floorService)
        {
            List<int> floorIds = new List<int>();
            foreach (Floor floor in floorService.getFloorForBuilding(buildingId))
            {
                floorIds.Add(floor.Id);
            }
            return repository.GetRoomsForFloors(floorIds);

        }

        public int mergeRooms(Room room1, Room room2, String name, String purpose)
        {
            int newIdIndex = repository.GetAll().Count - 1;
            int newId = repository.GetAll()[newIdIndex].Id + 97;
            mergeEquipment(room1.Id, room2.Id, newId);
            int Height;
            int Width;
            int X = room1.X < room2.X ? room1.X : room2.X;
            int Y = room1.Y < room2.Y ? room1.Y : room2.Y;
            if (room1.Vertical)
            {
                Width = room1.Width;
                Height = room1.Height + 10 + room2.Height;
            }
            else
            {
                Width = room1.Width + 10 + room2.Width;
                Height = room1.Height;
            }

            int FloorId = room1.FloorId;
            int DoorX = room1.DoorX;
            int DoorY = room1.DoorY;
            bool Vertical = room1.Vertical;
            bool DoorExist = room1.DoorExist;
            string Css = room1.Css;

            Room newRoom = new Room(newId, name, purpose, -1, FloorId, X, Y, Height, Width, DoorX, DoorY, Vertical, Css, DoorExist);
            repository.Delete(room1.Id);
            repository.Delete(room2.Id);
            repository.Save(newRoom);

            return newId;
        }

        public void splitRoom(Room room, String name1, String purpose1, String name2, String purpose2)
        {

            int newIdIndex = repository.GetAll().Count - 1;
            int newId = repository.GetAll()[newIdIndex].Id + 97;
            int newRoom1Id, newRoom2Id;
            string newRoom1Name, newRoom2Name;
            string newRoom1Purpose, newRoom2Purpose;
            int newRoom1X, newRoom2X;
            int newRoom1Y, newRoom2Y;
            int newRoom1Width, newRoom2Width;
            int newRoom1Height, newRoom2Height;
            int newRoom1FloorId, newRoom2FloorId;
            int newRoom1DoorX, newRoom2DoorX;
            int newRoom1DoorY, newRoom2DoorY;
            bool newRoom1Vertical, newRoom2Vertical;
            bool newRoom1DoorExist, newRoom2DoorExist;
            string newRoom1Css, newRoom2Css;
            //Ukoliko je soba uspravna dijelimo je na dvije uspravno
            if (room.Vertical)
            {
                newRoom1Id = room.Id;
                newRoom1Name = name1;
                newRoom1Purpose = purpose1;
                newRoom1X = room.X;
                newRoom1Y = room.Y;
                newRoom1Width = room.Width;
                if (room.Height % 2 == 0)
                {
                    newRoom1Height = room.Height / 2 - 5;
                }
                else
                {
                    newRoom1Height = (room.Height - 1) / 2 - 5;
                }
                newRoom1FloorId = room.FloorId;
                newRoom1DoorX = room.DoorX;
                newRoom1DoorY = newRoom1Y + 20;
                newRoom1Vertical = room.Vertical;
                newRoom1DoorExist = room.DoorExist;
                newRoom1Css = room.Css;

                newRoom2Id = newId;
                newRoom2Name = name2;
                newRoom2Purpose = purpose2;
                newRoom2X = room.X;
                if (room.Height % 2 == 0)
                {
                    newRoom2Y = room.Y + room.Height / 2 + 5;
                    newRoom2Height = room.Height / 2 - 5;
                }
                else
                {
                    newRoom2Y = room.Y + (room.Height - 1) / 2 + 5;
                    newRoom2Height = (room.Height - 1) / 2 - 4;
                }
                newRoom2Width = room.Width;
                newRoom2FloorId = room.FloorId;
                newRoom2DoorX = room.DoorX;
                newRoom2DoorY = newRoom2Y + 20;
                newRoom2Vertical = room.Vertical;
                newRoom2DoorExist = room.DoorExist;
                newRoom2Css = room.Css;
            }
            else //Ukoliko je vodoravna
            {
                newRoom1Id = room.Id;
                newRoom1Name = name1;
                newRoom1Purpose = purpose1;
                newRoom1X = room.X;
                newRoom1Y = room.Y;
                newRoom1Height = room.Height;
                if (room.Width % 2 == 0)
                {
                    newRoom1Width = room.Width / 2 - 5;
                }
                else
                {
                    newRoom1Width = (room.Width - 1) / 2 - 4;
                }
                newRoom1FloorId = room.FloorId;
                newRoom1DoorX = newRoom1X + 15;
                newRoom1DoorY = room.Y - 2;
                newRoom1Vertical = room.Vertical;
                newRoom1DoorExist = room.DoorExist;
                newRoom1Css = room.Css;

                newRoom2Id = newId;
                newRoom2Name = name2;
                newRoom2Purpose = purpose2;
                newRoom2Y = room.Y;
                if (room.Width % 2 == 0)
                {
                    newRoom2X = room.X + room.Width / 2 + 5;
                    newRoom2Width = room.Width / 2 - 5;
                }
                else
                {
                    newRoom2X = room.X + (room.Width - 1) / 2 + 5;
                    newRoom2Width = (room.Width - 1) / 2 - 4;
                }

                newRoom2Height = room.Height;
                newRoom2FloorId = room.FloorId;
                newRoom2DoorX = newRoom2X + 20;
                newRoom2DoorY = newRoom2Y - 2;
                newRoom2Vertical = room.Vertical;
                newRoom2DoorExist = room.DoorExist;
                newRoom2Css = room.Css;

            }
            Room newRoom1 = new Room(newRoom1Id, newRoom1Name, newRoom1Purpose, -1, newRoom1FloorId, newRoom1X, newRoom1Y, newRoom1Height, newRoom1Width, newRoom1DoorX, newRoom1DoorY, newRoom1Vertical, newRoom1Css, newRoom1DoorExist);
            Room newRoom2 = new Room(newRoom2Id, newRoom2Name, newRoom2Purpose, -1, newRoom2FloorId, newRoom2X, newRoom2Y, newRoom2Height, newRoom2Width, newRoom2DoorX, newRoom2DoorY, newRoom2Vertical, newRoom2Css, newRoom2DoorExist);
            repository.Delete(room.Id);
            repository.Save(newRoom1);
            repository.Save(newRoom2);

        }

        public bool Relocate(Equipment eqForTransf, int fromRoomId, int toRoomId)
        {
            foreach (Equipment equip in equipmentService.GetRoomEquipments(fromRoomId))
            {
                if (!(equip.Name.Equals(eqForTransf.Name) && equip.Amount > eqForTransf.Amount))
                    continue;

                Equipment fromRoomEquip = new Equipment(equip.Id, equip.Name, equip.Type, equip.Amount - eqForTransf.Amount, fromRoomId);
                equipmentService.Delete(equip.Id);
                equipmentService.Save(fromRoomEquip);
                Equipment toRoomEquip = new Equipment(eqForTransf.Id, eqForTransf.Name, eqForTransf.Type, eqForTransf.Amount, toRoomId);
                equipmentService.Save(toRoomEquip);
                return true;
            }

            return false;
        }

        public void mergeEquipment(int room1ID, int room2ID, int newId)
        {
            List<Equipment> equipInRoom1 = equipmentRepository.GetRoomEquipemnts(room1ID);
            List<Equipment> equipInRoom2 = equipmentRepository.GetRoomEquipemnts(room2ID);
            List<Equipment> newRoomEquipment = new List<Equipment>();
            string nameEq = "";
            if (equipInRoom1 == null) //Ukoliko u prvoj sobi nema opreme
            {
                foreach (Equipment e in equipInRoom2)
                {
                    Equipment eq = new Equipment(e.Id, e.Name, e.Type, e.Amount, newId);
                    equipmentRepository.Delete(e.Id);
                    equipmentRepository.Save(eq);
                }
                return;
            }

            if (equipInRoom2 != null && equipInRoom1 != null)
            {
                foreach (Equipment eq2 in equipInRoom2)
                {
                    foreach (Equipment eq1 in equipInRoom1)
                    {
                        if (eq1.Name.Equals(eq2.Name))
                        {
                            int sameName = eq1.Amount + eq2.Amount;
                            nameEq = eq1.Name;
                            Equipment e = new Equipment(eq1.Id, eq1.Name, eq1.Type, sameName, newId);
                            equipmentRepository.Delete(eq1.Id);
                            equipmentRepository.Save(e);
                            equipmentRepository.Delete(eq2.Id);
                            break;
                        }
                        else
                        {
                            if (nameEq != eq1.Name)
                            {
                                Equipment e1 = new Equipment(eq1.Id, eq1.Name, eq1.Type, eq1.Amount, newId);
                                equipmentRepository.Delete(eq1.Id);
                                equipmentRepository.Save(e1);
                            }
                            Equipment e = new Equipment(eq2.Id, eq2.Name, eq2.Type, eq2.Amount, newId);
                            equipmentRepository.Delete(eq2.Id);
                            equipmentRepository.Save(e);
                        }
                    }
                }
                return;
            }

            if (equipInRoom2 == null)
            {
                foreach (Equipment e in equipInRoom1)
                {
                    Equipment eq = new Equipment(e.Id, e.Name, e.Type, e.Amount, newId);
                    equipmentRepository.Delete(e.Id);
                    equipmentRepository.Save(eq);
                }
            }
        }

    }
}
