Room1 = {
	Person = {
		id= "test",
		Texture2 = {
			dimensions = {
				width = 100,
				height = 100,
			},
			src = 'test'
		},
		Location2 = {
			x = 100,
			y = 100,
			rotation = 1.4
		}
	},
    Person2 = {
        id="test2",
        Texture2 = {
            dimensions = {
                width = 200,
                height = 100,
            },
            src = 'test'
        },
        Location2 = {
            x=300,
            y=100,
            rotation = 1.4
        },
        Scripts = {
            playerstop = {
                moveSpeed = 5,
                src = "Scripts/test1.lua",
            }
        }
    }
}