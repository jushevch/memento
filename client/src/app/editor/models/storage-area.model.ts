import { AreaInfo } from "./area-info.model";
import { Area } from "./area.model";
import { StorageRoom } from "./storage-room.model";
import { Item } from "./item.model";
import { Mobile } from "./mobile.model";
import { ResetCommand } from "./reset-command.enum";

export class StorageArea
{
    info: AreaInfo;
    rooms: StorageRoom[];
    items: Item[];
    mobiles: Mobile[];
    resets: string[];

    constructor(area?: Area)
    {
        if (area)
        {
            this.info = area.info;
            this.rooms = new Array<StorageRoom>();
            this.items = new Array<Item>();
            this.mobiles = new Array<Mobile>();
            this.resets = new Array<string>();

            for (let room of area.rooms.values())
            {
                this.rooms.push(new StorageRoom(room));
            }

            for (let item of area.items.values())
            {
                this.items.push(item);
            }

            for (let mob of area.mobiles.values())
            {
                this.mobiles.push(mob);
            }

            for (let room of area.rooms.values())
            {
                for (let reset of room.itemResets)
                {
                    this.resets.push(`${ResetCommand.RepopItem} ${room.vnum} ${reset.item.vnum}`);
                }

                for (let reset of room.mobileResets)
                {
                    this.resets.push(`${ResetCommand.RepopMobile} ${room.vnum} ${reset.mobile.vnum}`);
                }
            }
        }
    }
}
