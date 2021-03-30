import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Area } from "../../models/area.model";
import { Room } from "../../models/room.model";

@Component({
    selector: "app-room",
    templateUrl: "./room.component.html"
})
export class RoomComponent
{
    @Input() area: Area;
    @Input() room: Room;

    @Output() goRoomEvent = new EventEmitter<number>();
    @Output() editItemEvent = new EventEmitter<number>();
    @Output() editMobileEvent = new EventEmitter<number>();

    currentPanel: Panel = Panel.Exits;
    areExitControlsVisible: boolean = false;

    constructor() { }

    toggleExitControls(): void
    {
        this.areExitControlsVisible = !this.areExitControlsVisible;
    }

    goRoom(vnum: number): void
    {
        this.goRoomEvent.emit(vnum);
    }

    editItem(vnum: number): void
    {
        this.editItemEvent.emit(vnum);
    }

    editMobile(vnum: number): void
    {
        this.editMobileEvent.emit(vnum);
    }

    goExitsPanel(): void
    {
        this.currentPanel = Panel.Exits;
    }

    goResetsPanel(): void
    {
        this.currentPanel = Panel.Resets;
    }
}

enum Panel
{
    Exits = 0,
    Resets = 1,
}
