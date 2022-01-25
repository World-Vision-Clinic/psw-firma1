export interface AppointmentForRoom {
    id: number;
    type: string;
    date: string;
  }

  export function emptyAppointment(): AppointmentForRoom {
    return {
      id: -1,
      type: "",
      date: ""
    };
}