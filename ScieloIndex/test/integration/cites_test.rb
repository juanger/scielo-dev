require "#{File.dirname(__FILE__)}/../test_helper"

class CitessTest < ActionController::IntegrationTest
  fixtures :cites

  def setup
    @cites = [:cite1, :cite2, :cite3]
  end

   def test_getting_index
     get "/cites"
     assert_equal 200, status
     assert_equal '/cites', path
   end

   def test_new
     get "/cites/new"
     assert_equal 200, status
     assert_equal '/cites/new', path
   end


   def  test_creating_new_collection
     post "cites/create", :record =>  {:article_id => 1, :cited_by_article_id => 5, :cite_order => 3}
     assert_equal 302, status
     follow_redirect!
     assert_equal '/cites/list', path
   end

   def test_showing
     @cites.each { | collection |
       post "/cites/show", :id => cites(collection).id
       assert 200, status
       assert_equal '/cites/show', path
     }
   end

   def test_editing
     @cites.each { | collection |
       post "/cites/edit", :id => cites(collection).id
       assert 200, status
       assert "/cites/edit/#{cites(collection).id}", path
     }
   end

   def test_updating_name
     @cites.each { | collection |
       post "/cites/update", :id => cites(collection).id, :cite_order => cites(collection).cite_order + 1
       assert 302, status
       follow_redirect!
       assert "/cites/show/#{cites(collection).id}", path
     }
   end

    def test_deleting
     @cites.each { | collection |
       get "/cites/destroy", :id => cites(collection).id
       assert 302, status
       assert '/cites/list',  path
     }
   end
end
