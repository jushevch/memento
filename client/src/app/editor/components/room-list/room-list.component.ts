import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Area } from "../../models/area.model";

@Component({
    selector: "app-room-list",
    templateUrl: "./room-list.component.html"
})
export class RoomListComponent
{
    @Input() area: Area;
    @Input() isCloneDisabled: boolean;

    @Output() editRoomEvent = new EventEmitter<number>();
    @Output() cloneRoomEvent = new EventEmitter<number>();
    @Output() roomDeletedEvent = new EventEmitter();

    constructor() { }

    editRoom(vnum: number): void
    {
        this.editRoomEvent.emit(vnum);
    }

    cloneRoom(vnum: number): void
    {
        if (!this.isCloneDisabled)
        {
            this.cloneRoomEvent.emit(vnum);
        }
    }

    deleteRoom(vnum: number): void
    {
        this.area.rooms.delete(vnum);
        this.roomDeletedEvent.emit();
    }
}
