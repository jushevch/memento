import { Direction } from "./direction.enum";

export class Exit
{
    direction: Direction;
    targetVnum: number;
    description: string;

    constructor(dir?: Direction)
    {
        if (dir)
        {
            this.direction = dir;
        }
    }
}
