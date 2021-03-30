import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Area } from "../../models/area.model";

@Component({
    selector: "app-mobile-list",
    templateUrl: "./mobile-list.component.html"
})
export class MobileListComponent
{
    @Input() area: Area;
    @Input() isCloneDisabled: boolean;

    @Output() editMobileEvent = new EventEmitter<number>();
    @Output() cloneMobileEvent = new EventEmitter<number>();
    @Output() mobileDeletedEvent = new EventEmitter();

    constructor() { }

    editMobile(vnum: number): void
    {
        this.editMobileEvent.emit(vnum);
    }

    cloneMobile(vnum: number): void
    {
        if (!this.isCloneDisabled)
        {
            this.cloneMobileEvent.emit(vnum);
        }
    }

    deleteMobile(vnum: number): void
    {
        this.deleteBoundResets(vnum);
        this.area.mobiles.delete(vnum);
        this.mobileDeletedEvent.emit();
    }

    private deleteBoundResets(vnum: number): void
    {
        for (let room of this.area.rooms.values())
        {
            while (1)
            {
                let reset = room.mobileResets.find(r => r.mobile.vnum == vnum);

                if (!reset)
                {
                    break;
                }

                let index = room.mobileResets.indexOf(reset);
                room.mobileResets.splice(index, 1);
            }
        }
    }
}
