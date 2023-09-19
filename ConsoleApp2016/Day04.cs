namespace ConsoleApp2016;

/*
 --- Day 4: Security Through Obscurity ---
Finally, you come across an information kiosk with a list of rooms. Of course, the list is encrypted and full of decoy data, but the instructions to decode the list are barely hidden nearby. Better remove the decoy data first.

Each room consists of an encrypted name (lowercase letters separated by dashes) followed by a dash, a sector ID, and a checksum in square brackets.

A room is real (not a decoy) if the checksum is the five most common letters in the encrypted name, in order, with ties broken by alphabetization. For example:

aaaaa-bbb-z-y-x-123[abxyz] is a real room because the most common letters are a (5), b (3), and then a tie between x, y, and z, which are listed alphabetically.
a-b-c-d-e-f-g-h-987[abcde] is a real room because although the letters are all tied (1 of each), the first five are listed alphabetically.
not-a-real-room-404[oarel] is a real room.
totally-real-room-200[decoy] is not.
Of the real rooms from the list above, the sum of their sector IDs is 1514.

What is the sum of the sector IDs of the real rooms?
 */

public class Day04 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 158835;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		foreach (var room in input)
		{
			return input.Where(r => r.IsValid()).Sum(r => r.SectorId);
		}
		 
		return 0;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 993;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		foreach (var room in input)
		{
			if (room.Decrypt().Contains("northpole"))
			{
				Console.WriteLine(room.Decrypt());
				return room.SectorId;
			}
		}

		return 0;
	}

	public Room Parse(string line)
	{
		return Room.Parse(line);
	}

}

/*
 --- Part Two ---
With all the decoy data out of the way, it's time to decrypt this list and get moving.

The room names are encrypted by a state-of-the-art shift cipher, which is nearly unbreakable without the right software. However, the information kiosk designers at Easter Bunny HQ were not expecting to deal with a master cryptographer like yourself.

To decrypt a room name, rotate each letter forward through the alphabet a number of times equal to the room's sector ID. A becomes B, B becomes C, Z becomes A, and so on. Dashes become spaces.

For example, the real name for qzmt-zixmtkozy-ivhz-343 is very encrypted name.

What is the sector ID of the room where North Pole objects are stored?
 */

public class Room
{
	public string EncryptedName { get; set; }
	public int SectorId { get; set; }
	public string Checksum { get; set; }

	public Room(string encryptedName, int sectorId, string checksum)
	{
		EncryptedName = encryptedName;
		SectorId = sectorId;
		Checksum = checksum;
	}

	public static Room Parse(string line)
	{
		line = line.Replace('[', '-').TrimEnd(']');
		var parts = line.Split('-');
		var encryptedName = parts.Take(parts.Length - 2).Aggregate((a, b) => $"{a}-{b}");
		var sectorId = int.Parse(parts[parts.Length - 2]);
		var checksum = parts[parts.Length - 1];

		return new Room(encryptedName, sectorId, checksum);
	}

	public bool IsValid()
	{
		var letters = EncryptedName.Replace("-", "").ToCharArray();
		var groups = letters.GroupBy(c => c).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Take(5);
		var checksum = groups.Aggregate("", (a, b) => $"{a}{b.Key}");

		return checksum == Checksum;
	}

	public string Decrypt()
	{
		var letters = EncryptedName.Replace("-", "").ToCharArray();
		var decrypted = new char[letters.Length];

		for (int i = 0; i < letters.Length; i++)
		{
			var letter = letters[i];
			var index = letter - 'a';
			var newIndex = (index + SectorId) % 26;
			var newLetter = (char) ('a' + newIndex);
			decrypted[i] = newLetter;
		}

		return new string(decrypted);
	}
}