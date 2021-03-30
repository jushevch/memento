import { Pipe, PipeTransform, Injectable } from "@angular/core";
import { EditorServicesModule } from "../editor-services.module";
import { Direction } from "../models/direction.enum";

@Pipe({
    name: "dirValue"
})
@Injectable({
    providedIn: EditorServicesModule
})
export class DirValuePipe implements PipeTransform
{
    private directions: Map<string, Direction> = new Map<string, Direction>([
        ["север", Direction.North],
        ["восток", Direction.East],
        ["юг", Direction.South],
        ["запад", Direction.West],
        ["вверх", Direction.Up],
        ["вниз", Direction.Down],
        ["куда-то", Direction.Somewhere],
        ["северо-восток", Direction.NorthEast],
        ["юго-восток", Direction.SouthEast],
        ["юго-запад", Direction.SouthWest],
        ["северо-запад", Direction.NorthWest],
    ]);

    transform(dirName: string): Direction
    {
        return this.directions.has(dirName) ? this.directions.get(dirName) : Direction.None;
    }
}
