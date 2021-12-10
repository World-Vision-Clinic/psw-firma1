using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Service
{
    public class RoomService
    {
        private RoomRepository repository;
        private EquipmentRepository equipmentRepository;
        public RoomService(RoomRepository repos)
        {
            repository = repos;
        }
        public RoomService(RoomRepository repos, EquipmentRepository equipRepo)
        {
            repository = repos;
            equipmentRepository = equipRepo;
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
            foreach(Floor floor in floorService.getFloorForBuilding(buildingId))
            {
                floorIds.Add(floor.Id);
            }
            return repository.GetRoomsForFloors(floorIds);

        }

        public int mergeRooms(Room room1, Room room2, String name, String purpose)
        {
            Room newRoom = new Room();
            int newId = repository.GetAll().Count + 150;
            mergeEquipment(room1.Id, room2.Id, newId);

            newRoom.Id = newId;
            newRoom.Name = name;
            newRoom.Purpose = purpose;
            newRoom.X = room1.X < room2.X ? room1.X : room2.X;
            newRoom.Y = room1.Y < room2.Y ? room1.Y : room2.Y;
            if (room1.Vertical)
            {
                newRoom.Width = room1.Width;
                newRoom.Height = room1.Height + 10 + room2.Height;
            } else
            {
                newRoom.Width = room1.Width + 10 + room2.Width;
                newRoom.Height = room1.Height;
            }
            
            newRoom.FloorId = room1.FloorId;
            newRoom.DoorX = room1.DoorX;
            newRoom.DoorY = room1.DoorY;
            newRoom.Vertical = room1.Vertical;
            newRoom.DoorExist = room1.DoorExist;
            newRoom.Css = room1.Css;
            newRoom.DoctorId = -1;
            repository.Delete(room1.Id);
            repository.Delete(room2.Id);
            repository.Save(newRoom);

            return newId;
        }

        public void splitRoom(Room room, String name1, String purpose1, String name2, String purpose2)
        {
            Room newRoom1 = new Room();
            Room newRoom2 = new Room();
            int newId=repository.GetAll().Count+15;
            //Ukoliko je soba uspravna dijelimo je na dvije uspravno
            if (room.Vertical)
            {
                newRoom1.Id = room.Id;
                newRoom1.Name = name1;
                newRoom1.Purpose = purpose1;
                newRoom1.X = room.X;
                newRoom1.Y = room.Y;
                newRoom1.Width = room.Width;
                if (room.Height % 2 == 0)
                {
                    newRoom1.Height = room.Height / 2 - 5;
                }
                else
                {
                    newRoom1.Height = (room.Height-1) / 2 - 5;
                }
                newRoom1.FloorId = room.FloorId;
                newRoom1.DoorX = room.DoorX;
                newRoom1.DoorY = newRoom1.Y+20;
                newRoom1.Vertical = room.Vertical;
                newRoom1.DoorExist = room.DoorExist;
                newRoom1.Css = room.Css;
                newRoom1.DoctorId = -1;

                newRoom2.Id = newId;
                newRoom2.Name = name2;
                newRoom2.Purpose = purpose2;
                newRoom2.X = room.X;
                if (room.Height % 2 == 0)
                {
                    newRoom2.Y = room.Y + room.Height / 2 + 5;
                    newRoom2.Height = room.Height / 2 - 5;
                } else
                {
                    newRoom2.Y = room.Y + (room.Height-1) / 2 + 5;
                    newRoom2.Height = (room.Height-1) / 2 - 4;
                }               
                newRoom2.Width = room.Width;
                newRoom2.FloorId = room.FloorId;
                newRoom2.DoorX = room.DoorX;
                newRoom2.DoorY = newRoom2.Y+20;
                newRoom2.Vertical = room.Vertical;
                newRoom2.DoorExist = room.DoorExist;
                newRoom2.Css = room.Css;
                newRoom2.DoctorId = -1;
            }
            else //Ukoliko je vodoravna
            {
                newRoom1.Id = room.Id;
                newRoom1.Name = name1;
                newRoom1.Purpose = purpose1;
                newRoom1.X = room.X;
                newRoom1.Y = room.Y;
                newRoom1.Height = room.Height;
                if (room.Width % 2 == 0)
                {
                    newRoom1.Width = room.Width / 2 - 5;
                }
                else
                {
                    newRoom1.Width = (room.Width - 1) / 2 - 4;
                }
                newRoom1.FloorId = room.FloorId;
                newRoom1.DoorX = newRoom1.X + 15;
                newRoom1.DoorY = room.Y-2;
                newRoom1.Vertical = room.Vertical;
                newRoom1.DoorExist = room.DoorExist;
                newRoom1.Css = room.Css;
                newRoom1.DoctorId = -1;

                newRoom2.Id = newId;
                newRoom2.Name = name2;
                newRoom2.Purpose = purpose2;
                newRoom2.Y = room.Y;
                if (room.Width % 2 == 0)
                {
                    newRoom2.X = room.X + room.Width / 2 + 5;
                    newRoom2.Width= room.Width / 2 - 5;
                }
                else
                {
                    newRoom2.X = room.X + (room.Width - 1) / 2 + 5;
                    newRoom2.Width = (room.Width - 1) / 2 - 4;
                }

                newRoom2.Height= room.Height;
                newRoom2.FloorId = room.FloorId;
                newRoom2.DoorX = newRoom2.X + 20;
                newRoom2.DoorY = newRoom2.Y-2;
                newRoom2.Vertical = room.Vertical;
                newRoom2.DoorExist = room.DoorExist;
                newRoom2.Css = room.Css;
                newRoom2.DoctorId = -1;
            }
            repository.Delete(room.Id);
            repository.Save(newRoom1);
            repository.Save(newRoom2);

        }

        public void mergeEquipment(int room1ID, int room2ID, int newId)
        {
            List<Equipment> equipInRoom1 = equipmentRepository.GetRoomEquipemnts(room1ID);
            List<Equipment> equipInRoom2 = equipmentRepository.GetRoomEquipemnts(room2ID);
            if (equipInRoom1 == null) //Ukoliko u prvoj sobi nema opreme
            {
                foreach(Equipment e in equipInRoom2)
                {
                    e.RoomId = newId;
                    equipmentRepository.Update(e);
                }
                return;
            }

            if (equipInRoom2 != null)
            {
                foreach (Equipment eq2 in equipInRoom2)
                {
                    foreach (Equipment eq1 in equipInRoom1)
                    {                      
                        if (eq1.Name == eq2.Name)
                        {
                            eq1.Amount += eq2.Amount;
                        }
                        else
                        {
                            eq2.RoomId = eq1.RoomId;
                        }
                    }
                }

                foreach (Equipment eq1 in equipInRoom1)
                {
                    eq1.RoomId = newId;
                    equipmentRepository.Update(eq1);
                }
                foreach (Equipment eq2 in equipInRoom2)
                {
                    eq2.RoomId = newId;
                    equipmentRepository.Update(eq2);
                }
                return;
            }

            if (equipInRoom2 == null)
            {
                return;
            }
        }

    }
}
