import { Component, Input, Output, EventEmitter } from "@angular/core";
import { GenderValuePipe } from "../../pipes/gender-value.pipe"
import { Gender } from "../../models/gender.enum";
import { Area } from "../../models/area.model";
import { Item } from "../../models/item.model";
import { Room } from "../../models/room.model";

@Component({
    selector: "app-item",
    templateUrl: "./item.component.html"
})
export class ItemComponent
{
    @Input() area: Area;
    @Input() item: Item;

    @Output() editRoomEvent = new EventEmitter<number>();

    genders: Gender[] = [
        Gender.Male,
        Gender.Female,
        Gender.Neutral,
        Gender.Plural,
    ];

    locations: Room[];

    constructor(
        private readonly genderValuePipe: GenderValuePipe,
    ) { }

    isLocationListVisible(): boolean
    {
        this.updateLocations();
        return this.locations.length > 0;
    }

    setItemGender(genderName: string): void
    {
        this.item.gender = this.genderValuePipe.transform(genderName);
    }

    editRoom(vnum: number): void
    {
        this.editRoomEvent.emit(vnum);
    }

    private updateLocations(): void
    {
        this.locations = new Array<Room>();

        for (let room of this.area.rooms.values())
        {
            for (let reset of room.itemResets)
            {
                if (reset.item.vnum == this.item.vnum)
                {
                    this.locations.push(room);
                }
            }
        }
    }
}
