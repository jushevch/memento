import { Direction } from "./direction.enum";
import { Exit } from "./exit.model";
import { ItemReset } from "./item-reset.model";
import { MobileReset } from "./mobile-reset.model";
import { StorageRoom } from "./storage-room.model";

export class Room
{
    vnum: number;
    title: string;
    look: string;
    exits: Map<Direction, Exit> = new Map<Direction, Exit>();
    itemResets: ItemReset[] = [];
    mobileResets: MobileReset[] = [];

    constructor(storage?: StorageRoom)
    {
        if (storage)
        {
            this.vnum = storage.vnum;
            this.title = storage.title;
            this.look = storage.look;

            for (let exit of storage.exits)
            {
                this.exits.set(exit.direction, exit);
            }
        }
    }
}
