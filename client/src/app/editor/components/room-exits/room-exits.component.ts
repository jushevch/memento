import { Component, Input, Output, EventEmitter, OnChanges } from "@angular/core";
import { DirValuePipe } from "../../pipes/dir-value.pipe";
import { OppositeDirValuePipe } from "../../pipes/opposite-dir-value.pipe";
import { NoTitlePipe } from "../../pipes/no-title.pipe";
import { Direction } from "../../models/direction.enum";
import { Area } from "../../models/area.model";
import { Room } from "../../models/room.model";
import { Exit } from "../../models/exit.model";

@Component({
    selector: "app-room-exits",
    templateUrl: "./room-exits.component.html"
})
export class RoomExitsComponent implements OnChanges
{
    private directions: Direction[] = [
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West,
        Direction.Up,
        Direction.Down,
        Direction.Somewhere,
        Direction.NorthEast,
        Direction.SouthEast,
        Direction.SouthWest,
        Direction.NorthWest,
    ];

    @Input() area: Area;
    @Input() room: Room;
    @Input() areControlsVisible: boolean;

    @Output() goRoomEvent = new EventEmitter<number>();

    currentExit: Exit = null;

    constructor(
        private readonly dirValuePipe: DirValuePipe,
        private readonly oppositeDirValuePipe: OppositeDirValuePipe,
        private readonly noTitlePipe: NoTitlePipe,
    ) { }

    ngOnChanges()
    {
        if (this.currentExit)
        {
            if (!this.room.exits.has(this.currentExit.direction))
            {
                this.clearCurrentExit();
            }
        }
    }

    getAvailableDirections(): Direction[]
    {
        let dirs: Direction[] = [];

        for (let dir of this.directions)
        {
            if (!this.room.exits.has(dir))
            {
                dirs.push(dir);
            }
        }

        return dirs;
    }

    toggleCurrentExit(dir: Direction): void
    {
        if (!this.currentExit || this.currentExit.direction != dir)
        {
            this.currentExit = this.room.exits.has(dir) ? this.room.exits.get(dir) : null;
        }
        else
        {
            this.clearCurrentExit();
        }
    }

    clearCurrentExit(): void
    {
        this.currentExit = null;
    }

    isCreateExitDisabled(): boolean
    {
        for (let dir of this.directions)
        {
            if (!this.room.exits.has(dir))
            {
                return false;
            }
        }

        return true;
    }

    createExit(dirName: string): void
    {
        let dir = this.dirValuePipe.transform(dirName);

        if (dir != Direction.None)
        {
            let exit = new Exit(dir);
            this.room.exits.set(exit.direction, exit);
        }
    }

    deleteExit(dir: Direction): void
    {
        this.room.exits.delete(dir);

        if (this.currentExit.direction == dir || this.room.exits.size == 0)
        {
            this.clearCurrentExit();
        }
    }

    getOppositeExit(): Exit
    {
        if (this.currentExit && this.area.rooms.has(this.currentExit.targetVnum))
        {
            let targetRoom = this.area.rooms.get(this.currentExit.targetVnum);
            let oppositeDir = this.oppositeDirValuePipe.transform(this.currentExit.direction);

            if (targetRoom.exits.has(oppositeDir))
            {
                let oppositeExit = targetRoom.exits.get(oppositeDir);

                if (oppositeExit.targetVnum == this.room.vnum)
                {
                    return oppositeExit;
                }
            }
        }

        return null;
    }

    isCreateOppositeExitDisabled(): boolean
    {
        if (!this.currentExit || !this.area.rooms.has(this.currentExit.targetVnum))
        {
            return true;
        }

        let targetRoom = this.area.rooms.get(this.currentExit.targetVnum);
        let oppositeDir = this.oppositeDirValuePipe.transform(this.currentExit.direction);

        return targetRoom.exits.has(oppositeDir);
    }

    createOppositeExit(): void
    {
        if (this.currentExit && this.area.rooms.has(this.currentExit.targetVnum))
        {
            let targetRoom = this.area.rooms.get(this.currentExit.targetVnum);
            let oppositeDir = this.oppositeDirValuePipe.transform(this.currentExit.direction);
            let exit = new Exit(oppositeDir);
            exit.targetVnum = this.room.vnum;
            targetRoom.exits.set(exit.direction, exit);
        }
    }

    isDeleteOppositeExitDisabled(): boolean
    {
        return !this.currentExit || this.getOppositeExit() == null;
    }

    deleteOppositeExit(): void
    {
        if (this.currentExit && this.area.rooms.has(this.currentExit.targetVnum))
        {
            let oppositeDir = this.oppositeDirValuePipe.transform(this.currentExit.direction);
            this.area.rooms.get(this.currentExit.targetVnum).exits.delete(oppositeDir);
        }
    }

    getRoomTitle(vnum: number): string
    {
        return this.area.rooms.has(vnum) ?
            this.noTitlePipe.transform(this.area.rooms.get(vnum).title) : "";
    }

    canGoRoom(vnum: number): boolean
    {
        return this.area.rooms.has(vnum);
    }

    goRoom(vnum: number): void
    {
        this.goRoomEvent.emit(vnum);
    }
}
