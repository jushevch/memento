import { Room } from "./room.model";
import { Exit } from "./exit.model";

export class StorageRoom
{
    vnum: number;
    title: string;
    look: string;
    exits: Exit[];

    constructor(room?: Room)
    {
        if (room)
        {
            this.vnum = room.vnum;
            this.title = room.title;
            this.look = room.look;
            this.exits = [];

            for (let exit of room.exits.values())
            {
                this.exits.push(exit);
            }
        }
    }
}
