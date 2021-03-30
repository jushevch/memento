import { Pipe, PipeTransform } from "@angular/core";
import { Direction } from "../models/direction.enum";

@Pipe({
    name: "dirName"
})
export class DirNamePipe implements PipeTransform
{
    private directions: Map<Direction, string> = new Map<Direction, string>([
        [Direction.North, "север"],
        [Direction.East, "восток"],
        [Direction.South, "юг"],
        [Direction.West, "запад"],
        [Direction.Up, "вверх"],
        [Direction.Down, "вниз"],
        [Direction.Somewhere, "куда-то"],
        [Direction.NorthEast, "северо-восток"],
        [Direction.SouthEast, "юго-восток"],
        [Direction.SouthWest, "юго-запад"],
        [Direction.NorthWest, "северо-запад"],
    ]);

    transform(dir: Direction): string
    {
        return this.directions.has(dir) ? this.directions.get(dir) :  "# # #";
    }
}
