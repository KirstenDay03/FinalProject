from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware
import pandas as pd
import csv
from typing import List
from pydantic import BaseModel

app = FastAPI()

app.add_middleware(
	CORSMiddleware,
	allow_origins=["*"],  
	allow_credentials=True,
	allow_methods=["*"],
	allow_headers=["*"],
)

try:
	df = pd.read_csv('Recipe.csv')
except:
	df = pd.DataFrame(columns=['id', 'name', 'prepTime','cookTime','ingredient1','ingredient2','ingredient3','ingredient4','ingredient5','instruction1','instruction2','instruction3','instruction4','instruction5'])
 
class Recipe(BaseModel):
	id: int
	name: str
	prepTime: str
	cookTime: str
	ingredient1: str
	ingredient2: str
	ingredient3: str
	ingredient4: str
	ingredient5: str
	instruction1: str
	instruction2: str
	instruction3: str
	instruction4: str
	instruction5: str
	

class RecipeList(BaseModel):
	recipe: list[Recipe]

@app.get("/")
async def root():
    return {"message": "Hello World"}

@app.get("/recipe")
async def get_recipe():
	return RecipeList(recipe=df.to_dict(orient='records'))
	

@app.get("/recipe/{id}")
async def get_recipe_by_id(id: int):
	recipe = df[df['id'] == id]

	if recipe.empty:
		raise HTTPException(status_code=404, detail="Recipe not found")

	return recipe.to_dict(orient='records')[0]

@app.post("/recipe")
async def add_recipe(recipe: Recipe):
	global df
	
	if recipe.id in df['id'].values:
		raise HTTPException(status_code=400, detail="Recipe already exists")

	new_recipe_df = pd.DataFrame([recipe.model_dump()])

	df = pd.concat([df, new_recipe_df], ignore_index=True)

	df.to_csv('Recipe.csv', index=False)

	return recipe

@app.put("/recipe/{id}")
async def update_recipe(id: int, recipe: Recipe):
	global df

	if id not in df['id'].values:
		raise HTTPException(status_code=404, detail="Recipe not found")

	df.loc[df['id'] == id, ['name','cuisine','prepTime','cookTime','ingredient1','ingredient2','ingredient3','ingredient4','ingredient5','instruction1','instruction2','instruction3','instruction4','instruction5']] = [recipe.name, recipe.cuisine, recipe.prepTime, recipe.cookTime, recipe.ingredient1, recipe.ingredient2,recipe.ingredient3, recipe.ingredient4, recipe.ingredient5, recipe.instruction1, recipe.instruction2, recipe.instruction3, recipe.instruction4, recipe.instruction5]

	# Save the dataframe to a csv file
	df.to_csv('Recipe.csv', index=False)

	return recipe

