import { Pipe, PipeTransform, Injectable } from "@angular/core";
import { EditorServicesModule } from "../editor-services.module";
import { Direction } from "../models/direction.enum";

@Pipe({
    name: "oppositeDirValue"
})
@Injectable({
    providedIn: EditorServicesModule
})
export class OppositeDirValuePipe implements PipeTransform
{
    private directions: Map<Direction, Direction> = new Map<Direction, Direction>([
        [Direction.North, Direction.South],
        [Direction.East, Direction.West],
        [Direction.South, Direction.North],
        [Direction.West, Direction.East],
        [Direction.Up, Direction.Down],
        [Direction.Down, Direction.Up],
        [Direction.Somewhere, Direction.Somewhere],
        [Direction.NorthEast, Direction.SouthWest],
        [Direction.SouthEast, Direction.NorthWest],
        [Direction.SouthWest, Direction.NorthEast],
        [Direction.NorthWest, Direction.SouthEast],
    ]);

    transform(dir: Direction): Direction
    {
        return this.directions.has(dir) ? this.directions.get(dir) : Direction.None;
    }
}
