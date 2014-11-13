using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace SpaceUnionXNA.Tools {
	/// <summary>
	/// This class cuts down on the amount of collision detections needed per object
	/// by eliminating objects that are too far away.
	/// Translated from Java code from:
	/// http://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374
	/// </summary>
	public class QuadTree {

		/// <summary>
		/// How many objects each node can hold.
		/// </summary>
		private const int MAX_OBJECTS = 10;
		/// <summary>
		/// Deepest level subnode
		/// </summary>
		private const int MAX_LEVELS = 5; // max objects 10*4*5=200?

		/// <summary>
		/// The current node level
		/// </summary>
		private int level;
		/// <summary>
		/// List of objects checking for collisions
		/// </summary>
		private List<Tangible> tangibles;
		/// <summary>
		/// The game area that this node represents
		/// </summary>
		private Rectangle bounds;
		/// <summary>
		/// The subnodes
		/// </summary>
		private QuadTree[] nodes;

		private List<Tangible> possibleCollisions;


		public QuadTree(int pLevel, Rectangle pBounds) {

			level = pLevel;
			tangibles = new List<Tangible>();
			bounds = pBounds;
			nodes = new QuadTree[4];

			possibleCollisions = new List<Tangible>();
		}

		/// <summary>
		/// Clears the QuadTree and it's nodes recursively.
		/// </summary>
		public void clear() {

			tangibles.Clear(); // empties the collision list

			for (int i = nodes.Length - 1; i >= 0; --i) {
				if (nodes[i] != null) {
					nodes[i].clear();
					nodes[i] = null;
				}
			}
		}

		/// <summary>
		/// Splits the node into subnodes.
		/// </summary>
		private void split() {

			int subWidth = (int) (bounds.Width / 2);
			int subHeight = (int) (bounds.Height / 2);
			int x = (int) bounds.X;
			int y = (int) bounds.Y;

			nodes[0] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
			nodes[1] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight));
			nodes[2] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
			nodes[3] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
		}

		/// <summary>
		/// Determine which node the object belongs to. -1 means
		/// object cannot completely fit within a child node and is part
		/// of the parent node.
		/// </summary>
		/// <param name="pRect"></param>
		/// <returns></returns>
		private int getIndex(Tangible tangible) {

			/** RightTop = 0, LeftTop = 1, LeftBottom = 2, RightBottom = 3 */
			int index =-1;
			double verticalMidpoint = bounds.X + bounds.Width / 2;
			double horizontalMidpoint = bounds.Y + bounds.Height / 2;

			// Object can completely fit within the top quadrants
			bool topQuadrant = (tangible.position.X < horizontalMidpoint
				&& tangible.position.Y + tangible.height < horizontalMidpoint);
			// Object can completely fit within the bottom quadrants
			bool bottomQuadrant = (tangible.position.Y > horizontalMidpoint);

			// Object can completely fit within the left quadrants
			if (tangible.position.X < verticalMidpoint && tangible.position.X + tangible.width < verticalMidpoint) {
				if (topQuadrant) {
					index = 1;
				} else if (bottomQuadrant) {
					index = 2;
				}
			}
				// Object can completely fit within the right quadrants
			 else if (tangible.position.X > verticalMidpoint) {
				if (topQuadrant) {
					index = 0;
				} else if (bottomQuadrant) {
					index = 3;
				}
			}

			return index;
		}

		/// <summary>
		/// Inserts object into quadtree. If the node exceeds capacity it will
		/// split and add all objects to their corresponging nodes
		/// </summary>
		/// <param name="pRect"></param>
		public void insert(Tangible tangible) {

			if (nodes[0] != null) { // if child nodes exist
				int index = getIndex(tangible); // into which node should this go?

				if (index != -1) {
					nodes[index].insert(tangible);

					return;
				}
			}

			tangibles.Add(tangible);

			if (tangibles.Count > MAX_OBJECTS && level < MAX_LEVELS) { // if this node is fulll
				if (nodes[0] == null) {
					split(); // create new child nodes if not done already
				}

				//cycle through all objects in this node
				for (int i = tangibles.Count - 1; i >= 0; --i) {
					Tangible tang = tangibles[i];
					int index = getIndex(tang);

					if (index != -1) {// into which node should this go?
						nodes[index].insert(tang); // place object in proper child node
						tangibles.Remove(tang); // and remove from this one
					}
				}
			}
		}

		/// <summary>
		/// Return all objects close enough that could potentially collide with this actor.
		/// </summary>
		/// <param name="actor"></param>
		/// <returns></returns>
		public List<Tangible> retrieve(Tangible actor) {

			int index = getIndex(actor);
			possibleCollisions.Clear();

			if (index != -1 && nodes[0] != null)
				possibleCollisions.AddRange(nodes[index].retrieve(actor));

			possibleCollisions.AddRange(tangibles);

			return possibleCollisions;
		}

		/// <summary>
		/// This works the same as retrieve(Tangible) but retrieves all the actors in 
		/// neighbour nodes as well.
		/// </summary>
		/// <param name="actor"></param>
		/// <returns></returns>
		public List<Tangible> retrieveNeighbors(Tangible actor) {

			/** RightTop = 0, LeftTop = 1, LeftBottom = 2, RightBottom = 3 */
			int index = getIndex(actor);
			possibleCollisions.Clear();

			if (index != -1 && nodes[0] != null) {

				int neigbour1 = index - 1;
				int neigbour2 = index + 1;
				if (neigbour1 < 0)
					neigbour1 = 3;
				if (neigbour2 > 3)
					neigbour2 = 0;

				possibleCollisions.AddRange(nodes[neigbour1].retrieve(actor));
				possibleCollisions.AddRange(nodes[index].retrieve(actor));
				possibleCollisions.AddRange(nodes[neigbour2].retrieve(actor));
			}

			possibleCollisions.AddRange(tangibles);

			return possibleCollisions;

		}
	}
}
