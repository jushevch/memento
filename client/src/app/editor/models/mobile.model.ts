import { Gender } from "./gender.enum";
import { Name } from "./name.model";

export class Mobile
{
    vnum: number;
    maxPerWorld: number = 1;
    keywords: string;
    gender: Gender = Gender.Male;
    name: Name = new Name();
    look: string;
    description: string;
}
