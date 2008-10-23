require "#{File.dirname(__FILE__)}/../test_helper"

class CollectionsTest < ActionController::IntegrationTest
  fixtures :countries, :publishers, :collections

  def setup
    @collections = [:atmosfera, :medicina]
  end

   def test_getting_index
     get "/collections"
     assert_equal 200, status
     assert_equal '/collections', path
   end

   def test_showing
     @collections.each { | collection |
       post "/collections/show", :id => collections(collection).id
       assert 200, status
       assert_equal '/collections/show', path
     }
   end

   def test_editing
     @collections.each { | collection |
       post "/collections/edit", :id => collections(collection).id
       assert 200, status
       assert "/collections/edit/#{collections(collection).id}", path
     }
   end

   def test_updating_name
     @collections.each { | collection |
       post "/collections/update", :id => collections(collection).id, :title => collections(collection).title.reverse, :state => collections(collection).state.reverse, :city => collections(collection).city.reverse, :url => collections(collection).url.reverse
       assert 302, status
       follow_redirect!
       assert "/collections/show/#{collections(collection).id}", path
     }
   end

    def test_deleting
     @collections.each { | collection |
       get "/collections/destroy", :id => collections(collection).id
       assert 302, status
       assert '/collections/list',  path
     }
   end
end
