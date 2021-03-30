import { Component, Input, Output, EventEmitter } from "@angular/core";
import { GenderValuePipe } from "../../pipes/gender-value.pipe"
import { Gender } from "../../models/gender.enum";
import { Area } from "../../models/area.model";
import { Mobile } from "../../models/mobile.model";
import { Room } from "../../models/room.model";

@Component({
    selector: "app-mobile",
    templateUrl: "./mobile.component.html"
})
export class MobileComponent
{
    @Input() area: Area;
    @Input() mobile: Mobile;

    @Output() editRoomEvent = new EventEmitter<number>();

    genders: Gender[] = [
        Gender.Male,
        Gender.Female,
        Gender.Neutral,
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

    setMobileGender(genderName: string): void
    {
        this.mobile.gender = this.genderValuePipe.transform(genderName);
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
            for (let reset of room.mobileResets)
            {
                if (reset.mobile.vnum == this.mobile.vnum)
                {
                    this.locations.push(room);
                }
            }
        }
    }
}
