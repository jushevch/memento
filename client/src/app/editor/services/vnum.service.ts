import { Injectable } from "@angular/core";
import { EditorServicesModule } from "../editor-services.module";

@Injectable({
    providedIn: EditorServicesModule
})
export class VnumService
{
    constructor() { }

    isVnumFree(vnum: number, vnums: IterableIterator<number>): boolean
    {
        for (let value of vnums)
        {
            if (vnum == value)
            {
                return false;
            }
        }

        return true;
    }

    getFirstFreeVnum(vnums: IterableIterator<number>, min: number, max: number): number
    {
        const arr = this.getSortedArray(vnums);
        const none: number = -1;

        if (min < 1 || max < min)
        {
            return none;
        }

        let vnum = min;

        for (let value of arr)
        {
            if (value < min || value > max)
            {
                return none;
            }

            if (vnum != value)
            {
                return vnum;
            }

            vnum++;
        }

        return vnum > max || vnum < min ? none : vnum;
    }

    private getSortedArray(vnums: IterableIterator<number>): number[]
    {
        let arr: number[] = [];

        for (let vnum of vnums)
        {
            arr.push(vnum);
        }

        return arr.sort((a, b) =>
        {
            return a > b ? 1 : a < b ? -1 : 0;
        });
    }
}
