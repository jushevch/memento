import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Area } from "../../models/area.model";
import { Room } from "../../models/room.model";
import { ItemReset } from "../../models/item-reset.model";
import { MobileReset } from "../../models/mobile-reset.model";

@Component({
    selector: "app-room-resets",
    templateUrl: "./room-resets.component.html"
})
export class RoomResetsComponent
{
    @Input() area: Area;
    @Input() room: Room;
    @Input() areControlsVisible: boolean;

    @Output() editItemEvent = new EventEmitter<number>();
    @Output() editMobileEvent = new EventEmitter<number>();

    constructor() { }

    createItemReset(vnum: string)
    {
        let item = this.area.items.get(+vnum);
        this.room.itemResets.push(new ItemReset(this.getNextResetId(), item));
        this.room.itemResets.sort((a, b) =>
        {
            return a.item.vnum > b.item.vnum ? 1 : a.item.vnum < b.item.vnum ? -1 : 0;
        });
    }

    createMobileReset(vnum: string)
    {
        let mob = this.area.mobiles.get(+vnum);
        this.room.mobileResets.push(new MobileReset(this.getNextResetId(), mob));
        this.room.mobileResets.sort((a, b) =>
        {
            return a.mobile.vnum > b.mobile.vnum ? 1 : a.mobile.vnum < b.mobile.vnum ? -1 : 0;
        });
    }

    isResetListAvailable(): boolean
    {
        return this.room.itemResets.length > 0 || this.room.mobileResets.length > 0;
    }

    editItem(vnum: number): void
    {
        this.editItemEvent.emit(vnum);
    }

    editMobile(vnum: number): void
    {
        this.editMobileEvent.emit(vnum);
    }

    deleteItemReset(id: number): void
    {
        let reset = this.room.itemResets.find(r => r.id == id);
        let index = this.room.itemResets.indexOf(reset);
        this.room.itemResets.splice(index, 1);
    }

    deleteMobileReset(id: number): void
    {
        let reset = this.room.mobileResets.find(r => r.id == id);
        let index = this.room.mobileResets.indexOf(reset);
        this.room.mobileResets.splice(index, 1);
    }

    private getNextResetId(): number
    {
        let id = 1;

        for (let reset of this.room.itemResets)
        {
            if (reset.id >= id)
            {
                id = reset.id + 1;
            }
        }

        for (let reset of this.room.mobileResets)
        {
            if (reset.id >= id)
            {
                id = reset.id + 1;
            }
        }

        return id;
    }
}
