import { Gender } from "./gender.enum";
import { Name } from "./name.model";

export class Item
{
    vnum: number;
    keywords: string;
    gender: Gender = Gender.Male;
    name: Name = new Name();
    look: string;
    description: string;
}
