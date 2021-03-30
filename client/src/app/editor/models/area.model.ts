import { StorageArea } from "./storage-area.model";
import { AreaInfo } from "./area-info.model";
import { Room } from "./room.model";
import { Item } from "./item.model";
import { Mobile } from "./mobile.model";
import { ResetCommand } from "./reset-command.enum";
import { ItemReset } from "./item-reset.model";
import { MobileReset } from "./mobile-reset.model";

export class Area
{
    info: AreaInfo = new AreaInfo();
    rooms: Map<number, Room> = new Map<number, Room>();
    items: Map<number, Item> = new Map<number, Item>();
    mobiles: Map<number, Mobile> = new Map<number, Mobile>();

    constructor() { }

    initialize(storage: StorageArea)
    {
        this.info = storage.info;
        this.rooms.clear();
        this.items.clear();
        this.mobiles.clear();

        this.initializeEntities(storage);
        this.initializeResets(storage);
    }

    private initializeEntities(storage: StorageArea): void
    {
        for (let roomFile of storage.rooms)
        {
            this.rooms.set(roomFile.vnum, new Room(roomFile));
        }

        for (let item of storage.items)
        {
            this.items.set(item.vnum, item);
        }

        for (let mob of storage.mobiles)
        {
            this.mobiles.set(mob.vnum, mob);
        }
    }

    private initializeResets(storage: StorageArea): void
    {
        let id = 1;

        for (let instruction of storage.resets)
        {
            let args = instruction.split(" ");
            let comm: ResetCommand = +args[0];
            let room = this.rooms.get(+args[1]);

            if (comm == ResetCommand.RepopItem)
            {
                room.itemResets.push(new ItemReset(id++, this.items.get(+args[2])));
            }

            if (comm == ResetCommand.RepopMobile)
            {
                room.mobileResets.push(new MobileReset(id++, this.mobiles.get(+args[2])));
            }
        }
    }
}
