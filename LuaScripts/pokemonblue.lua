local inputFile = "input.txt";

function readFrom(file)
	local f = io.open(inputFile, "rb");
	if not f then return "WAIT" end
	local content = f:read "*a";
	f:close();
	return content;
end



while true do
	local line = readFrom(inputFile);

	if line ~= "WAIT" then
		console.log(line);
		joypad.set({[line] = true}, null);
		emu.frameadvance();
		joypad.set({[line] = false}, null);

		os.remove(inputFile);
	end

	emu.frameadvance();
end
