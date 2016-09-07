using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileManager {

	public void Save(string path, object data) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.OpenWrite(path);

		bf.Serialize(file, data);
		file.Close();
	}

	public object Load(string path) {
		object data = null;

		if (File.Exists(path)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.OpenRead(path);
			data = bf.Deserialize(file);
			file.Close();
		}

		return data;
	}
}
