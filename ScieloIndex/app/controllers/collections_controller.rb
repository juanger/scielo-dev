class CollectionsController < ScieloIndexController
  def initialize
    @model = Collection
    @created_msg = 'The collection was created!'
    @updated_msg = 'The collection was updated!'
  end
end
